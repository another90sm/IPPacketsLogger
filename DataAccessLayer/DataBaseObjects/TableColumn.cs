using System;
using DataAccess.Enums;

namespace DataAccess.DataBaseObjects
{
    [Serializable]
    public class TableColumn
    {
        public string Name { get; set; }
        public ColumnType Type { get; set; }
        public bool AllowNull { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }
        public int Precision { get; set; }
        public TableColumn()
        {

        }
    }
}
