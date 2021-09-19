using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KztekKeyRegister.Tools
{
    public static class ConfigsManager<T> where T : class
    {
        // Load configfile
        public static T LoadConfig(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader srReader = File.OpenText(filePath)) 
                    {
                        if (String.IsNullOrEmpty(srReader.ReadLine()))
                        {
                            return null;
                        }
                        Type tType = typeof(T);
                        System.Xml.Serialization.XmlSerializer xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                        object oData = xsSerializer.Deserialize(srReader);
                        var config = (T)oData;
                        srReader.Close();
                        if (config != null)
                            return config;
                        else
                            return null;
                    } ;
                }
                return null;
            }
            catch (Exception )
            {

                throw;
            }
        }

        // Save configfile
        public static void SaveConfig(T config, string filePath)
        {
            try
            {
                using(StreamWriter swWriter = File.CreateText(filePath))
                {
                    Type tType = config.GetType();
                    System.Xml.Serialization.XmlSerializer xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                    xsSerializer.Serialize(swWriter, config);
                    swWriter.Close();
                }
             
            }
            catch (Exception)
            {
                throw ;
            }
        }
    }
}
