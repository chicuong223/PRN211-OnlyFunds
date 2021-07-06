using BusinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PostDAO
    {
        private static PostDAO instance = null;
        private static readonly object instanceLock = new object();
        public static PostDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new PostDAO();
                }
                return instance;
            }
        }

        public int CountPostsByUser(User user)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                int count = context.Posts.Where(post => post.UploaderUsername.Equals(user.Username)).Count();
                return count;
            }
            catch
            {
                throw new Exception("Error counting posts");
            }
        }
        public IEnumerable<Post> GetPostsByUser(User user, int pageIndex)
        {
            List<Post> posts = new List<Post>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                SqlConnection con = (SqlConnection)context.Database.GetDbConnection();
                string SQL = "SELECT * FROM \n"
                    + "(SELECT ROW_NUMBER() OVER(ORDER BY PostId DESC) AS r, * \n"
                    + "FROM Post WHERE UploaderUsername = @username) as x \n"
                    + "where x.r between @index * 3 - (3 - 1) AND 3 * @index";
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@index", pageIndex);
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
                            UploaderUsernameNavigation = user
                        };
                        posts.Add(post);
                    }
                    reader.NextResult();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return posts;
        }

        public void InsertPost(Post post)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                context.Posts.Add(post);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeletePost(int postID)
        {
            try
            {
                Post post = GetPostByID(postID);
                if (post == null)
                {
                    throw new Exception("This post does not exist");
                }
                using var context = new PRN211_OnlyFunds_CopyContext();
                context.Posts.Remove(post);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Post GetPostByID(int postID)
        {
            Post post = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                post = context.Posts.Find(postID);
            }
            catch
            {
                throw new Exception("Error finding Post");
            }
            return post;
        }

        public IEnumerable<Post> SearchPostByTitle(string title)
        {
            var posts = new List<Post>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                posts = context.Posts.Where(p => p.PostTitle.ToLower().Contains(title.ToLower())).ToList();
                if (posts == null)
                {
                    throw new Exception("There's no post with this title");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return posts;
        }
    }
}
