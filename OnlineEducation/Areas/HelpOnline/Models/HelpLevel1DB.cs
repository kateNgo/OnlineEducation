using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class HelpLevel1DB
    {
        string cs = ConfigurationManager.ConnectionStrings["ConStringSqlServer"].ConnectionString;
        // return a list of help level1
        public List<HelpLevel1> ListAll()
        {
            string query = "SELECT * FROM HelpOnlineLevel1 order by IndexTopic ASC";
            List<HelpLevel1> list = new List<HelpLevel1>();
            using (SqlConnection con = new SqlConnection(cs))
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
                            HelpLevel1 obj = new HelpLevel1();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
                            obj.Index = Convert.ToInt32(row["IndexTopic"]);
                            list.Add(obj);
                        }
                    }
                }

            }

            return list;
        }
        public List<HelpLevel1> ListAllDESC()
        {
            string query = "SELECT * FROM HelpOnlineLevel1 order by IndexTopic DESC";
            List<HelpLevel1> list = new List<HelpLevel1>();
            using (SqlConnection con = new SqlConnection(cs))
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
                            HelpLevel1 obj = new HelpLevel1();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
                            obj.Index = Convert.ToInt32(row["IndexTopic"]);
                            list.Add(obj);
                        }
                    }
                }

            }

            return list;
        }
        //Method for Adding HelpLevel1
        public int InsertLevel1(HelpLevel1 level1)
        {
            string query = "sp_AddHelpOnlineLevel1"; // Stored procedure in DB
            int new_ID = -1;
            using (SqlConnection con = new SqlConnection(cs))
            {

                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //SqlParameter outPutVal = new SqlParameter("@NewId", SqlDbType.Int);
                   
                    cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = level1.Title;
                    cmd.Parameters.Add("@imageFile", SqlDbType.VarChar).Value = (level1.ImageFile == null?"": level1.ImageFile);
                    cmd.Parameters.Add("@indexTopic", SqlDbType.Int).Value = level1.Index;
                    cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    new_ID = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                    return new_ID;
                }
            }
        }
        
        //Method for Update Index of a topic help level 1
        public void UpdateIndexTopicLevel1(HelpLevel1 level1)
        {
            string query = "UPDATE HelpOnlineLevel1 SET IndexTopic=@indexTopic WHERE Id=@Id";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.AddWithValue("@indexTopic", level1.Index);
                    cmd.Parameters.AddWithValue("@Id", level1.Id);
                    cmd.Connection = con;
                    con.Open();
                    //int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
        /**
         * This method is to return last index of help online level1 
         **/
        public int GetLastIndexHelpLevel1()
        {
            string query = "SELECT * FROM HelpOnlineLevel1 order by IndexTopic DESC";
            using (SqlConnection con = new SqlConnection(cs))
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
        // Delete Help Level 1 by ID
        public void DeleleHelpLevel1(int id)
        {
            string query = "DELETE FROM HelpOnlineLevel1 WHERE Id=@Id";
            using (SqlConnection con = new SqlConnection(cs))
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
        // Delete all records in Help Level1 table
        public void DeleleAllHelpLevel1()
        {
            string query = "DELETE FROM HelpOnlineLevel1 ";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    // cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        // Get Help Level 1 by Id
        public HelpLevel1 GetHelpLevel1ById(int id)
        {

            string query = "SELECT * FROM HelpOnlineLevel1 WHERE Id = " + id;
            using (SqlConnection con = new SqlConnection(cs))
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
                            HelpLevel1 obj = new HelpLevel1();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
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
        // Get Help Level1 by Title
        public HelpLevel1 GetHelpLevel1ByTitle(string title)
        {
            string query = "SELECT * FROM HelpOnlineLevel1 WHERE title = '" + title + "'";
            using (SqlConnection con = new SqlConnection(cs))
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
                            HelpLevel1 obj = new HelpLevel1();
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Title = row["Title"].ToString();
                            obj.ImageFile = row["ImageFile"].ToString();
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
        public void UpdateLevel1(HelpLevel1 level1)
        {
            string query = "UPDATE HelpOnlineLevel1 SET Title = @title, ImageFile = @imageFile, IndexTopic=@indexTopic WHERE Id=@Id";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@title", level1.Title);
                    cmd.Parameters.AddWithValue("@imageFile", level1.ImageFile);
                    cmd.Parameters.AddWithValue("@indexTopic", level1.Index);
                    cmd.Parameters.AddWithValue("@Id", level1.Id);
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