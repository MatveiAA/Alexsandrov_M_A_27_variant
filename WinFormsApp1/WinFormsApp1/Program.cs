using System;
using System.Windows.Forms;
using ElectronicsStore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicsStore
{
    internal static class Program
    {
        // —оздаем статическое свойство, чтобы формы имели доступ к контейнеру
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // »нициализируем наш IoC-контейнер
            ServiceProvider = DependencyContainer.Configure();

            // «апускаем главную форму
            Application.Run(new MainForm());
        }
    }
}