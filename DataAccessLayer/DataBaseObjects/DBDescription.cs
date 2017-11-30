using DataAccess.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace DataAccessLayer.DataBaseObjects
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

        public static DBDescription DeserializeFromXML()
        {
            Assembly resourceAssembly = Assembly.Load("Resources");
            string xmlFilePath = resourceAssembly
                .GetManifestResourceNames()
                .Where(x => x == "Resources.DBResources.DBObjectsDescriptions.xml")
                .FirstOrDefault();

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
