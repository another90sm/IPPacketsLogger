using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DataAccess.DataBaseObjects
{
    [Serializable]
    [XmlInclude(typeof(TableColumn))]
    internal class Table
    {
        public string TableName { get; set; }
        public List<TableColumn> TableColumns{ get; set; }
        public Table()
        {

        }
    }
}
