using ElectronicsStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElectronicsStore.Repositories;

internal static class RepositoryOperation
{
    internal static T Run<T>(IDbContextFactory<DataContext> factory, ILogger logger, string operation,
        int? id, Func<DataContext, T> action)
    {
        logger.LogDebug("Операция {Operation}, идентификатор {EntityId}", operation, id);
        try
        {
            using var context = factory.CreateDbContext();
            return action(context);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogWarning(ex, "Конфликт при операции {Operation}, идентификатор {EntityId}", operation, id);
            throw new InvalidOperationException("Запись уже изменена. Обновите данные и повторите операцию.", ex);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Отклонена операция {Operation}, идентификатор {EntityId}", operation, id);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при операции {Operation}, идентификатор {EntityId}", operation, id);
            throw;
        }
    }
}
