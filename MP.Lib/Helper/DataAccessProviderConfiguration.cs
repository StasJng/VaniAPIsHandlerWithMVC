using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Xml;

namespace MP.Lib.Helper
{
    public class DataAccessProviderConfiguration
    {
        private Hashtable providers = new Hashtable();
        private string defaultProvider;
        public Hashtable Providers
        {
            get { return this.providers; }
        }
        public string DefaultProvider
        {
            get
            {
                return this.defaultProvider;
            }
        }
        public static DataAccessProviderConfiguration GetDataAccessProviderConfiguration()
        {
            object config = WebConfigurationManager.GetSection("VmgPortal/data");
            return (DataAccessProviderConfiguration)config;
        }

        public void LoadValuesFromConfigurationXml(XmlNode node)
        {
            XmlAttributeCollection attributeCollection = node.Attributes;
            //  Get the default provider
            defaultProvider = attributeCollection["defaultProvider"].Value;
            //  ' Read child nodes
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "providers")
                {
                    GetProviders(child);
                }
            }
        }
        public void GetProviders(XmlNode node)
        {
            foreach (XmlNode providerNode in node.ChildNodes)
            {
                switch (providerNode.Name)
                {
                    case "add":
                        providers.Add(providerNode.Attributes["name"].Value, new Provider(providerNode.Attributes["name"].Value, providerNode.Attributes["type"].Value, providerNode.Attributes["connectionString"].Value));
                        break;
                    case "remove":
                        providers.Remove(providerNode.Attributes["name"].Value);
                        break;
                    case "clear":
                        providers.Clear();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public class Provider
    {
        private string providerName;
        private string providerType;
        private string connectionString;

        public Provider(string _name, string _type, string connStr)
        {
            this.providerName = _name;
            this.ProviderType = _type;
            this.ConnectionString = connStr;
        }
        public Provider(string _name, string _type, ITransactionManager _Session)
        {
            this.providerName = _name;
            this.ProviderType = _type;
            this.Session = _Session;
        }
        public string ProviderName
        {
            get { return this.providerName; }
            set { this.providerName = value; }
        }
        public string ProviderType
        {
            get { return this.providerType; }
            set { this.providerType = value; }
        }
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
            set
            {
                this.connectionString = value;
            }
        }
        public ITransactionManager Session { get; set; }
    }
}
