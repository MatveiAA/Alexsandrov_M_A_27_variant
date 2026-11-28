namespace ElectronicsStore;

internal sealed class StoreGrid<T> : DataGridView where T : class
{
    public StoreGrid(params (string Header, Func<T, object?> Value)[] columns)
    {
        Dock = DockStyle.Fill;
        ReadOnly = true;
        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        RowHeadersVisible = false;
        MultiSelect = false;
        SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _columns = columns;
        foreach (var (title, _) in columns)
            Columns.Add(new DataGridViewTextBoxColumn { HeaderText = title, SortMode = DataGridViewColumnSortMode.NotSortable });
    }

    private readonly (string Header, Func<T, object?> Value)[] _columns;
    public T? SelectedItem => CurrentRow?.Tag as T;

    public void SetItems(IEnumerable<T> items)
    {
        Rows.Clear();
        foreach (var item in items)
        {
            var index = Rows.Add(_columns.Select(column => column.Value(item)).ToArray());
            Rows[index].Tag = item;
        }
    }

    public static FlowLayoutPanel Toolbar() => new() { Dock = DockStyle.Bottom, Height = 44, Padding = new Padding(5) };

    public static Button Action(string label, EventHandler action)
    {
        var button = new Button { Text = label, AutoSize = true };
        button.Click += action;
        return button;
    }
}
