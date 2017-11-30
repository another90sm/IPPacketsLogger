using System;
using System.Xml.Serialization;

namespace DataAccess.DataBaseObjects
{
    public enum ColumnType
    {
        [XmlEnum(Name ="text")]
        Text = 0,
        [XmlEnum(Name = "integer")]
        Integer = 1,
        [XmlEnum(Name = "real")]
        Real = 2,
        [XmlEnum(Name = "none")]
        None = 3,
        [XmlEnum(Name = "numeric")]
        Numeric = 4
    }
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
