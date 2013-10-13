using System;
using System.Windows.Controls;
using Telerik.Data;

namespace SilverlightDataTable
{
    public partial class Page : UserControl
    {
        string[] names = new string[] { "Côte de Blaye", "Boston Crab Meat", 
            "Singaporean Hokkien Fried Mee", "Gula Malacca", "Rogede sild", 
            "Spegesild", "Zaanse koeken", "Chocolade", "Maxilaku", "Valkoinen suklaa" };
        decimal[] prizes = new decimal[] { 23.25M, 9.00M, 45.60M, 32.00M, 
            14.00M, 19.00M, 263.50M, 18.40M, 3.00M, 14.00M };
        bool[] bools = new bool[] { true, false, true, false, true, false, true, false, true, false };
        DataTable table = null;

        private readonly Random rnd = new Random();

        public Page()
        {
            InitializeComponent();

            var table = new DataTable();
            table.Columns.Add(new DataColumn() { ColumnName = "ID", DataType = typeof(int) });
            table.Columns.Add(new DataColumn() { ColumnName = "Name", DataType = typeof(string) });
            table.Columns.Add(new DataColumn() { ColumnName = "UnitPrice", DataType = typeof(decimal) });
            table.Columns.Add(new DataColumn() { ColumnName = "Date", DataType = typeof(DateTime) });
            table.Columns.Add(new DataColumn() { ColumnName = "Discontinued", DataType = typeof(bool) });

            for (int i = 0; i < 5; i++)
            {
                var row = table.NewRow();
                row["ID"] = i;
                row["Name"] = names[rnd.Next(9)];
                row["UnitPrice"] = prizes[rnd.Next(9)];
                row["Date"] = DateTime.Now.AddDays(i);
                row["Discontinued"] = bools[rnd.Next(9)];
                table.Rows.Add(row);
            }

            DataContext = table;
        }

        private void Button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var table = (DataTable)DataContext;

            var row = table.NewRow();
            row["ID"] = table.Rows.Count;
            row["Name"] = names[rnd.Next(9)];
            row["UnitPrice"] = prizes[rnd.Next(9)];
            row["Date"] = DateTime.Now.AddDays(table.Rows.Count);
            row["Discontinued"] = bools[rnd.Next(9)];
            table.Rows.Add(row);
        }

        private void Button2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var table = (DataTable)DataContext;

            table.Rows.Remove(table.Rows[RadGridView1.Items.IndexOf(RadGridView1.CurrentItem)]);
        }
    }
}
