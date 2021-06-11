using System.Windows.Forms;

namespace IPScanner.Utility
{
    class ComponentUtils
    {
        public static void DataGridViewCellAutoSize(DataGridView dataGridView)
        {
            if (dataGridView.Columns.Count > 0)
            {
                for (int i = 0; i <= dataGridView.Columns.Count - 1; i++)
                {
                    dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
        }

        public static void ListViewCellAutoSize(ListView listView)
        {
            if (listView.Columns.Count > 0)
            {
                for (int i = 0; i <= listView.Columns.Count - 1; i++)
                {
                    listView.Columns[i].Width = -2;
                }
            }
        }
    }
}
