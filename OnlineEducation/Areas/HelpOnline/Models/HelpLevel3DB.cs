using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class HelpLevel3DB
    {
        public SqlConnection SQLConnect()
        {
            string connString = WebConfigurationManager.ConnectionStrings["ConStringSqlServer"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            //conn.Open();
            return conn;
        }
        public void InsertLevel3(HelpLevel3 level3)
        {
            string query = "INSERT INTO HelpOnlineLevel3 (Title, URL, ParentId, IndexTopic) VALUES (@title,@url,  @parentId, @indexTopic);";
            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@title", level3.Title);
                    cmd.Parameters.AddWithValue("@url", level3.URL);
                    cmd.Parameters.AddWithValue("@parentId", level3.ParentId);
                    cmd.Parameters.AddWithValue("@indexTopic", level3.Index);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
        public void DeleleLevel3(int id)
        {
            string query = "DELETE FROM HelpOnlineLevel3 WHERE Id=@Id";
            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public List<HelpLevel3> ListAll()
        {
            string query = "SELECT * FROM HelpOnlineLevel3 order by parentId, IndexTopic";
            List<HelpLevel3> list = new List<HelpLevel3>();
            using (SqlConnection con = SQLConnect())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        con.Open();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow row = dt.Rows[i];
                            HelpLevel3 obj = new HelpLevel3();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.URL = row["URL"].ToString();
                            obj.ParentId = Convert.ToInt32(row["ParentId"]);
                            obj.Index = Convert.ToInt32(row["IndexTopic"]);
                            list.Add(obj);

                        }

                    }
                }
            }
            return list;

        }
        public HelpLevel3 GetHelpLevel3ById(int id)
        {

            string query = "SELECT * FROM HelpOnlineLevel3 WHERE Id = " + id;
            using (SqlConnection con = SQLConnect())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        con.Open();

                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            HelpLevel3 obj = new HelpLevel3();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.URL = row["URL"].ToString();
                            obj.ParentId = Convert.ToInt32(row["ParentId"]);
                            obj.Index = Convert.ToInt32(row["IndexTopic"]);
                            return obj;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
            }

        }
        public List<HelpLevel3> LoadHelpLevel3ByParentId(int parentId)
        {
            string query = "SELECT * FROM HelpOnlineLevel3 WHERE parentID =" + parentId + " order by IndexTopic";
            List<HelpLevel3> list = new List<HelpLevel3>();
            using (SqlConnection con = SQLConnect())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        con.Open();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow row = dt.Rows[i];
                            HelpLevel3 level3 = new HelpLevel3();
                            level3.Id = Convert.ToInt32(row["Id"]);
                            level3.Title = row["Title"].ToString();
                            level3.URL = row["URL"].ToString();
                            level3.ParentId = Convert.ToInt32(row["parentId"]);
                            HelpLevel2DB helpLevel2DB = new HelpLevel2DB();
                            level3.ParentTopic = helpLevel2DB.GetHelpLevel2ById(level3.ParentId);
                            level3.Index = 1;
                            list.Add(level3);

                        }

                    }
                }
            }
            return list;
        }
        public void UpdateLevel3(HelpLevel3 level3)
        {
            string query = "UPDATE HelpOnlineLevel3 SET Title = @title,URL = @url, ParentId = @parentId, IndexTopic=@indexTopic WHERE Id=@Id";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@title", level3.Title);
                    cmd.Parameters.AddWithValue("@url", level3.URL);
                    cmd.Parameters.AddWithValue("@parentId", level3.ParentId);
                    cmd.Parameters.AddWithValue("@indexTopic", level3.Index);
                    cmd.Parameters.AddWithValue("@Id", level3.Id);
                    cmd.Connection = con;
                    con.Open();
                    //int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
        public HelpLevel3 GetHelpLevel3ByTitleAndParentId(string title, int id)
        {
            string query = "SELECT * FROM HelpOnlineLevel3 WHERE title = '" + title + "' AND parentId = " + id;
            using (SqlConnection con = SQLConnect())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        con.Open();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            HelpLevel3 obj = new HelpLevel3();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.URL = row["URL"].ToString();
                            obj.ParentId = Convert.ToInt32(row["ParentId"]);
                            //ParentTopic = getHelpLevel2ById(ParentId),
                            obj.Index = Convert.ToInt32(row["IndexTopic"]);
                            return obj;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
            }
        }
        public void UpdateIndexTopicAndURLLevel3(HelpLevel3 level3)
        {
            string query = "UPDATE HelpOnlineLevel3 SET IndexTopic=@indexTopic, URL=@url WHERE Id=@Id";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.AddWithValue("@indexTopic", level3.Index);
                    cmd.Parameters.AddWithValue("@url", level3.URL);
                    cmd.Parameters.AddWithValue("@Id", level3.Id);
                    cmd.Connection = con;
                    con.Open();
                    //int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
        public void UpdateIndexTopicLevel3(HelpLevel3 level3)
        {
            string query = "UPDATE HelpOnlineLevel3 SET IndexTopic=@indexTopic WHERE Id=@Id";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.AddWithValue("@indexTopic", level3.Index);
                    cmd.Parameters.AddWithValue("@Id", level3.Id);
                    cmd.Connection = con;
                    con.Open();
                    //int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
    }
}