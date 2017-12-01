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
        Numeric = 4
    }
}
