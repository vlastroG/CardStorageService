using DotNetSettings.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DotNetSettings
{
    public class CacheProvider
    {
        private static byte[] _additionalEntropy = { 3, 1, 4, 1, 5, 9, 2, 6, 5 };

        private readonly string _fileName = "data.protected";

        public void CacheConnections(List<ConnectionString> connections)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                using MemoryStream memoryStream = new MemoryStream();
                using XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xmlSerializer.Serialize(xmlTextWriter, connections);

                byte[] protectedData = Protect(memoryStream.ToArray());
                File.WriteAllBytes(_fileName, protectedData);
            }
            catch (Exception)
            {
                throw new SerializeDataCacheProviderException();
            }
        }

        private byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, _additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException)
            {
                throw new ProtectCacheProviderException();
            }
        }

        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, _additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception)
            {
                throw new UnprotectCacheProviderException();
            }
        }

        public List<ConnectionString> GetConnectionsFromCeche()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                byte[] protectedData = File.ReadAllBytes(_fileName);
                byte[] data = Unprotect(protectedData);
                return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(data));
            }
            catch (Exception)
            {
                throw new DeserializeCacheProviderException();
            }
        }
    }
}
