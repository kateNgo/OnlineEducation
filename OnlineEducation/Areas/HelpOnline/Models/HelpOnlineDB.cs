using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class HelpOnlineDB
    {
        HelpLevel1DB helpLevel1BD = new HelpLevel1DB();
        HelpLevel2DB helpLevel2BD = new HelpLevel2DB();
        HelpLevel3DB helpLevel3BD = new HelpLevel3DB();
        public SqlConnection SQLConnect()
        {
            string connString = WebConfigurationManager.ConnectionStrings["ConStringSqlServer"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            //conn.Open();
            return conn;
        }
        public void UpdateIndexTopicToZero()
        {
            string query = "UPDATE HelpOnlineLevel1 SET IndexTopic=0 ; UPDATE HelpOnlineLevel2 SET IndexTopic=0 ; UPDATE HelpOnlineLevel3 SET IndexTopic=0 ; ";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void UpdateDBWithXML(List<HelpLevel1> listOfLevel1FromXMLFile, string serverPathToSaveHTML, string serverPathOfHTMLLink)
        {

            foreach (HelpLevel1 level1 in listOfLevel1FromXMLFile)
            {
                HelpLevel1 level1DB = helpLevel1BD.GetHelpLevel1ByTitle(level1.Title);
                if (level1DB == null)
                {
                    // add new this level1 
                    level1.Id = helpLevel1BD.InsertLevel1(level1);

                }
                else
                {
                    // this topic existed, just update index topic
                    level1DB.Index = level1.Index;
                    level1.Id = level1DB.Id;
                    helpLevel1BD.UpdateIndexTopicLevel1(level1DB);
                }
                UpdateDBWithXMLLevel2(level1, serverPathToSaveHTML, serverPathOfHTMLLink);
            }
            DeleteIndexTopicZero();
        }

        public void UpdateDBWithXMLLevel2(HelpLevel1 level1, string serverPathToSaveHTML, string serverPathOfHTMLLink)
        {
            foreach (HelpLevel2 level2 in level1.Children)
            {
                level2.ParentId = level1.Id;
                HelpLevel2 level2DB = helpLevel2BD.GetHelpLevel2ByTitleAndParentId(level2.Title, level1.Id);
                if (level2DB == null)
                {
                    // add new this level1 
                    level2.Id = helpLevel2BD.InsertLevel2(level2);
                }
                else
                {
                    // this topic existed, just update index topic
                    level2DB.Index = level2.Index;
                    level2.Id = level2DB.Id;
                    helpLevel2BD.UpdateIndexTopicLevel2(level2DB);
                }
                UpdateDBWithXMLLevel3(level2, serverPathToSaveHTML, serverPathOfHTMLLink);
            }
        }
        public void UpdateDBWithXMLLevel3(HelpLevel2 level2, string serverPathToSaveHTML, string serverPathOfHTMLLink)
        {
            foreach (HelpLevel3 level3 in level2.Children)
            {
                level3.ParentId = level2.Id;
                HelpLevel3 level3DB = helpLevel3BD.GetHelpLevel3ByTitleAndParentId(level3.Title, level2.Id);
                if (level3DB == null)
                {
                    // add new this level1 
                    helpLevel3BD.InsertLevel3(level3);
                    // upload htm file
                    XMLHanlder.DownloadHTMLFile(level3.URL, serverPathToSaveHTML, serverPathOfHTMLLink);
                }
                else
                {
                    // this topic existed, just update index topic
                    level3DB.Index = level3.Index;
                    level3.Id = level3DB.Id;
                    if (!level3DB.URL.Equals(level3.URL))
                    {
                        level3DB.URL = level3.URL;
                        XMLHanlder.DownloadHTMLFile(level3.URL, serverPathToSaveHTML, serverPathOfHTMLLink);
                        helpLevel3BD.UpdateIndexTopicAndURLLevel3(level3DB);
                    }
                    else
                    {
                        helpLevel3BD.UpdateIndexTopicLevel3(level3DB);
                    }
                }
            }
        }

        public void DeleteIndexTopicZero()
        {
            string query = "DELETE FROM HelpOnlineLevel1 WHERE IndexTopic=0;DELETE FROM HelpOnlineLevel2 WHERE IndexTopic=0;DELETE FROM HelpOnlineLevel3 WHERE IndexTopic=0;";
            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}