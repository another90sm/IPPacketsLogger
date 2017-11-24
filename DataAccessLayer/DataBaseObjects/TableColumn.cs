using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string ColumnName { get; set; }
        public ColumnType ColumnType { get; set; }
        public bool AllowNull { get; set; }
        public bool IsPrimaryKey { get; set; }

        public TableColumn()
        {

        }
    }
}
