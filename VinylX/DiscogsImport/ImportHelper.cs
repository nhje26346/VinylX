using System.Xml;

namespace VinylX.DiscogsImport
{
    public class ImportHelper
    {
        public void SplitXml(string filepathToSplit, string rootNodeName, string nodeToSplitName, int numberOfNodes, string filepath)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filepathToSplit);

            bool moreNodes = true;
            int nodesProcessed = 0;
            int documentsProcessed = 1;
            XmlDocument splitDocument;

            XmlElement root = document.DocumentElement;
            XmlNodeList nodes = root.SelectNodes(nodeToSplitName);

            splitDocument = new XmlDocument();
            XmlNode splitRootNode = splitDocument.CreateElement(rootNodeName);
            splitDocument.AppendChild(splitRootNode);
            foreach (XmlNode node in nodes)
            {
               
                // new document
                if (nodesProcessed == numberOfNodes) 
                {
                    splitDocument.Save(filepath+"/"+ rootNodeName+""+ documentsProcessed+".xml");            
                    documentsProcessed++;

                    splitDocument = new XmlDocument();
                    splitRootNode = splitDocument.CreateElement(rootNodeName);
                    splitDocument.AppendChild(splitRootNode);
                    nodesProcessed = 0;
                }
                else
                {
                    XmlNode copiedNode = splitDocument.ImportNode(node, true);
                    splitRootNode.AppendChild(copiedNode);
                    nodesProcessed++;
                }
            }
            splitDocument.Save(filepath + "/" + rootNodeName + "" + documentsProcessed + ".xml");
        }
    }
}
