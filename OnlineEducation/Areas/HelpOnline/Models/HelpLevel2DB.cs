using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class HelpLevel2DB
    {
        public SqlConnection SQLConnect()
        {
            string connString = WebConfigurationManager.ConnectionStrings["ConStringSqlServer"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            //conn.Open();
            return conn;
        }
        /**
         * This method is to return last index of help online level2 belong to a parent topic level 1 
         **/
        public int GetLastIndexHelpLevel2ByParentId(int parentId)
        {
            string query = "SELECT * FROM HelpOnlineLevel2 where parentId = " + parentId + " order by IndexTopic DESC";
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

                            return Convert.ToInt32(row["IndexTopic"]);

                        }
                        else
                        {
                            return 0;
                        }

                    }
                }
            }
        }
        public HelpLevel2 GetHelpLevel2ByTitle(string title)
        {
            string query = "SELECT * FROM HelpOnlineLevel2 WHERE title = '" + title + "'";
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
                            HelpLevel2 obj = new HelpLevel2();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
                            obj.Index = Convert.ToInt32(row["IndexTopic"]);
                            obj.ParentId = Convert.ToInt32(row["ParentId"]);
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
        public int InsertLevel2(HelpLevel2 level2)
        {
            string query = "INSERT INTO HelpOnlineLevel2 (Title,IndexTopic, ParentId) VALUES (@title,  @indexTopic, @parentId);SELECT SCOPE_IDENTITY();";
            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@title", level2.Title);
                    //cmd.Parameters.AddWithValue("@imageFile", level2.ImageFile);
                    cmd.Parameters.AddWithValue("@indexTopic", level2.Index);
                    cmd.Parameters.AddWithValue("@parentId", level2.ParentId);
                    cmd.Connection = con;
                    con.Open();
                    int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return insertedID;
                }
            }
        }
        public void UpdateIndexTopicLevel2(HelpLevel2 level2)
        {
            string query = "UPDATE HelpOnlineLevel2 SET IndexTopic=@indexTopic WHERE Id=@Id";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.AddWithValue("@indexTopic", level2.Index);
                    cmd.Parameters.AddWithValue("@Id", level2.Id);
                    cmd.Connection = con;
                    con.Open();
                    //int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
        public void UpdateLevel2(HelpLevel2 level2)
        {
            string query = "UPDATE HelpOnlineLevel2 SET Title = @title, ParentId = @parentId, IndexTopic=@indexTopic WHERE Id=@Id";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@title", level2.Title);
                    cmd.Parameters.AddWithValue("@parentId", level2.ParentId);
                    cmd.Parameters.AddWithValue("@indexTopic", level2.Index);
                    cmd.Parameters.AddWithValue("@Id", level2.Id);
                    cmd.Connection = con;
                    con.Open();
                    //int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
        public void DeleleLevel2(int id)
        {
            string query = "DELETE FROM HelpOnlineLevel2 WHERE Id=@Id";
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
        public List<HelpLevel2> ListAll()
        {
            string query = "SELECT * FROM HelpOnlineLevel2 order by parentId, IndexTopic";
            List<HelpLevel2> list = new List<HelpLevel2>();
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
                            HelpLevel2 obj = new HelpLevel2();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
                            obj.ParentId = Convert.ToInt32(row["ParentId"]);
                            obj.Index = Convert.ToInt32(row["IndexTopic"]);
                            list.Add(obj);

                        }

                    }
                }
            }
            return list;

        }
        public List<HelpLevel2> LoadHelpLevel2ByParentId(int parentId)
        {
            if (parentId == 0)
            {
                return ListAll();
            }
            string query = "SELECT * FROM HelpOnlineLevel2 where parentId = " + parentId + " order by IndexTopic";
            List<HelpLevel2> list = new List<HelpLevel2>();
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
                            HelpLevel2 obj = new HelpLevel2();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
                            obj.ParentId = Convert.ToInt32(row["ParentId"]);
                            obj.Index = Convert.ToInt32(row["IndexTopic"]);
                            list.Add(obj);

                        }

                    }
                }
            }
            return list;
        }
        public HelpLevel2 GetHelpLevel2ById(int id)
        {
            

            string query = "SELECT * FROM HelpOnlineLevel2 WHERE Id = " + id;
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
                            HelpLevel2 obj = new HelpLevel2();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
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
        public HelpLevel2 GetHelpLevel2ByTitleAndParentId(string title, int parentId)
        {
            string query = "SELECT * FROM HelpOnlineLevel2 WHERE title = '" + title + "' AND parentId = " + parentId;
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
                            HelpLevel2 obj = new HelpLevel2();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
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
    }
}