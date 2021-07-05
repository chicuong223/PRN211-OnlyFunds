using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    class PostCategoryMapDAO
    {
        private static PostCategoryMapDAO instance = null;
        private static readonly object instanceLock = new object();

        public static PostCategoryMapDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostCategoryMapDAO();
                    }

                    return instance;
                }
            }
        }

        public IEnumerable<Post> FilterPostByCategory(int categoryId, int pageIndex)
        {
           
            var posts = new List<Post>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                SqlConnection con = (SqlConnection)context.Database.GetDbConnection();
                string SQL =
                    "SELECT * FROM Post WHERE PostId IN (SELECT PostId FROM PostCategoryMap WHERE CategoryId = ?)";
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int postID = reader.GetInt32(1);
                        string title = reader.GetString(2);
                        string desc = reader.GetString(3);
                        string fileURL = reader.GetString(4);
                        string uploaderUsername = reader.GetString(5);
                        DateTime date = reader.GetDateTime(6);
                        Post post = new Post
                        {
                            PostId = postID,
                            PostTitle = title,
                            PostDescription = desc,
                            FileUrl = fileURL,
                            UploadDate = date,
                            UploaderUsername = uploaderUsername
                        };
                        posts.Add(post);
                    }

                    reader.NextResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return posts;
        }

        public void AddPostMap(int postId, int categoryId)
        {
            try
            {
                PostCategoryMap map= GetPostMap(postId, categoryId);
                if (map == null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.PostCategoryMaps.Add(map);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public PostCategoryMap GetPostMap(int postId, int categoryId)
        {
            PostCategoryMap map = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                map = context.PostCategoryMaps.SingleOrDefault(m => m.CategoryId == categoryId && m.PostId == postId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return map;
        }

    }
}
