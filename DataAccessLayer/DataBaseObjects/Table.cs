using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace DataAccess.DataBaseObjects
{
    [Serializable]
    [XmlInclude(typeof(TableColumn))]
    public class Table
    {
        public string Name { get; set; }
        public List<TableColumn> Columns { get; set; }
        public Table()
        {

        }
        public Table(string tableName, IEnumerable<TableColumn> tableColumns)
        {
            this.Name = tableName;
            this.Columns = tableColumns.ToList();
        }
    }
}
