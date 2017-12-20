using System.Xml.Serialization;

namespace DataAccess.Enums
{
    public enum ColumnType
    {
        [XmlEnum(Name = "text")]
        Text = 0,
        [XmlEnum(Name = "integer")]
        Integer = 1,
        [XmlEnum(Name = "real")]
        Real = 2,
        [XmlEnum(Name = "none")]
        None = 3,
        [XmlEnum(Name = "numeric")]
        Numeric = 4,
        [XmlEnum(Name = "varchar")]
        Varchar = 5,
        [XmlEnum(Name = "nvarchar")]
        Nvarchar = 6,
        [XmlEnum(Name = "blob")]
        Blob = 7,
        [XmlEnum(Name = "timestamp")]
        Timestamp = 8,
    }
}
