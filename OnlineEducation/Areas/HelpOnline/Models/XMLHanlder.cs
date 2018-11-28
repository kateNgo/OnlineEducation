using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class XMLHanlder
    {
        public static List<HelpLevel1> ReadXML(string fileXML, string serverPathToSaveHTML, string serverPathOfHTMLLink)
        {
            List<HelpLevel1> listOfLevel1 = new List<HelpLevel1>();
            try
            {

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                settings.IgnoreProcessingInstructions = true;
                settings.IgnoreWhitespace = true;
                XmlReader r = XmlReader.Create(fileXML, settings);
                while (r.NodeType != XmlNodeType.Element)
                {
                    r.Read();
                }

                XElement e = XElement.Load(r);
                if (e.HasElements)
                {
                    IEnumerable<XElement> level1Topics = e.Elements();
                    int indexLevel1 = 0;
                    foreach (XElement el1 in level1Topics)
                    {
                        if (el1.Name.ToString().Equals("item", StringComparison.OrdinalIgnoreCase))
                        {
                            XElement itemLevel1 = XElement.Parse(el1.ToString());
                            HelpLevel1 level1 = new HelpLevel1();
                            indexLevel1 += 1;
                            level1.Title = itemLevel1.Attribute("name").Value;
                            level1.Index = indexLevel1;
                            level1.ImageFile = "";
                            level1.Children = new List<HelpLevel2>();
                            // Handling level 2
                            IEnumerable<XElement> level2Topics = el1.Elements();
                            int indexLevel2 = 0;
                            foreach (XElement el2 in level2Topics)
                            {
                                if (el2.Name.ToString().Equals("item", StringComparison.OrdinalIgnoreCase))
                                {
                                    XElement itemLevel2 = XElement.Parse(el2.ToString());
                                    HelpLevel2 level2 = new HelpLevel2();
                                    indexLevel2 += 1;
                                    level2.Title = itemLevel2.Attribute("name").Value;
                                    level2.Index = indexLevel2;
                                    
                                    level2.ImageFile = "";
                                    level2.Children = new List<HelpLevel3>();
                                    // Hanlde level 3
                                    if ((itemLevel2.Attribute("link") != null) && (itemLevel2.Attribute("link").Value != null) && (!itemLevel2.Attribute("link").Value.Equals("")))
                                    {
                                        // this node does not have child so this node is also level 3 with url is link value
                                        HelpLevel3 level3 = new HelpLevel3();
                                        level3.Title = level2.Title;
                                        level3.URL = itemLevel2.Attribute("link").Value;
                                        
                                        level3.Index = 1;
                                        level2.Children.Add(level3);

                                        // download html file here
                                        //DownloadHTMLFile(level3.URL,serverPathToSaveHTML,serverPathOfHTMLLink);
                                    }
                                    else
                                    {
                                        // has level 3
                                        IEnumerable<XElement> level3Topics = el2.Elements();
                                        foreach (XElement el3 in level3Topics)
                                        {
                                            if (el3.Name.ToString().Equals("item", StringComparison.OrdinalIgnoreCase))
                                            {
                                                XElement itemLevel3 = XElement.Parse(el3.ToString());
                                                HelpLevel3 level3 = new HelpLevel3();
                                                level3.Title = itemLevel3.Attribute("name").Value;
                                                level3.URL = itemLevel3.Attribute("link").Value;
                                                
                                                level3.Index = 1;
                                                level2.Children.Add(level3);
                                                // download html file here
                                                // DownloadHTMLFile(level3.URL, serverPathToSaveHTML, serverPathOfHTMLLink);
                                            }
                                        }
                                    }

                                    level1.Children.Add(level2);
                                }
                            }
                            //Add level 1
                            listOfLevel1.Add(level1);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.Write(ex.Message);
            }
            return listOfLevel1;

        }

        public static string UploadXMLFile(FileUpload fileUpload, string path)
        {
            string fileName = "";
            try
            {
                fileName = path + Path.GetFileName(fileUpload.FileName);
                fileUpload.SaveAs(fileName);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return fileName;
        }
        public static void DownloadHTMLFile(string url, string fileName)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(url, fileName);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }
        public static void DownloadHTMLFile(string linkHTMLFile, string serverPathToSaveHTML, string serverPathOfHTMLLink)
        {
            string folderPath = GetFolderFromUrl(linkHTMLFile);
            if (!Directory.Exists(serverPathToSaveHTML + folderPath))
            {
                Directory.CreateDirectory(serverPathToSaveHTML + folderPath);
            }
            string htmlFileNameToSave = serverPathToSaveHTML + linkHTMLFile;
            string urlLink = serverPathOfHTMLLink + linkHTMLFile;
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(urlLink, htmlFileNameToSave);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            Console.Write("aaaa");
        }
        public static string GetFilenameFromUrl(string url)
        {
            int indexOf = url.LastIndexOf("\\");
            if (indexOf == -1)
                return url;
            string s = url.Substring(indexOf);
            return s;

        }
        public static string GetFolderFromUrl(string url)
        {
            int indexOf = url.LastIndexOf("\\");
            if (indexOf == -1)
                return "";
            string s = url.Substring(0, indexOf);
            return s;

        }
        public static void UploadImageFiles(string clientPath)
        {


        }
    }
}