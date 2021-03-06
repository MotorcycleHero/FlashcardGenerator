﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace FlashcardGenerator
{
    abstract public class GeneratorXmlFile : XmlDocument
    {
        #region Constructors
        private GeneratorXmlFile() {
        }

        public GeneratorXmlFile(string inputDirectory) {
            baseDirectory = inputDirectory;
        }
        #endregion

        #region Properties
        public string BaseDirectory {
            get {
                return baseDirectory;
            }
        }

        public abstract string FileName { get; }
        #endregion

        protected XmlNode GetXmlNode(string xpath) {
            var output = this.SelectSingleNode(xpath);

            if (output != null) {
                return output;
            }
            else {
                throw new XmlException(string.Format("\"{0}\" does not contain a \"{1}\" node.", this.FileName, xpath));
            }
        }

        public static List<XmlNode> GetXmlNodeList(XmlNode parentNode, string xpath) {
            XmlNodeList xmlNodeList = parentNode.SelectNodes(xpath);
            var output = new List<XmlNode>();

            if (xmlNodeList != null) {
                foreach (XmlNode xmlNode in xmlNodeList) {
                    output.Add(xmlNode);
                }
            }

            return output;
        }

        public static string GetAttributeValue(XmlNode xmlNode, string name) {
            var attribute = xmlNode.Attributes[name];
            if (attribute != null) {
                return attribute.Value.Trim();
            }
            else {
                return "";
            }
        }

        public static int GetIntFromAttribute(XmlNode xmlNode, string name, int defaultValue) {
            string attributeValue = GetAttributeValue(xmlNode, name);
            int output = defaultValue;

            if (attributeValue != "") {
                int.TryParse(attributeValue, out output);
            }

            return output;
        }

        private string baseDirectory = null;
    }
}
