using DataAccess.DataBaseObjects;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace DataAccess.DataBaseObjects
{
    [Serializable]
    [XmlRoot]
    [XmlInclude(typeof(Table))]
    public class DBDescription
    {
        public List<Table> Tables { get; set; }
        public DBDescription()
        {

        }

        public DBDescription(IEnumerable<Table> tables)
        {
            this.Tables = tables.ToList();
        }
        /// <summary>
        /// Not needed for now
        /// </summary>
        /// <param name="dbDescription"></param>
        public static void SerializeToXML(DBDescription dbDescription)
        {

        }

        public static DBDescription DeserializeFromXML(DataBaseType dbType)
        {
            Assembly resourceAssembly = Assembly.Load("Resources");
            string xmlFilePath = string.Empty;

            switch (dbType)
            {
                case DataBaseType.SQLite:
                    xmlFilePath = resourceAssembly
                .GetManifestResourceNames()
                .Where(x => x == "Resources.DBResources.SQLiteDBObjectsDescription.xml")
                .FirstOrDefault();
                    break;
                case DataBaseType.MySQL:
                    xmlFilePath = resourceAssembly
                .GetManifestResourceNames()
                .Where(x => x == "Resources.DBResources.MySQLDBObjectsDescription.xml")
                .FirstOrDefault();
                    break;
                case DataBaseType.MSSQL:
                    xmlFilePath = resourceAssembly
                .GetManifestResourceNames()
                .Where(x => x == "Resources.DBResources.MSSQLDBObjectsDescription.xml")
                .FirstOrDefault();
                    break;
                case DataBaseType.Oracle:
                    xmlFilePath = resourceAssembly
                .GetManifestResourceNames()
                .Where(x => x == "Resources.DBResources.OracleDBObjectsDescription.xml")
                .FirstOrDefault();
                    break;
            }


            var serializer = new XmlSerializer(typeof(DBDescription));
            Stream stream;

            using (stream = resourceAssembly.GetManifestResourceStream(xmlFilePath))
            {
                using (var xmlReader = XmlReader.Create(stream))
                {
                    return (DBDescription)serializer.Deserialize(xmlReader);
                }
            }
        }
    }
}
