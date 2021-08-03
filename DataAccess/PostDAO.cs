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
        //-------Checked
        public int CountPostsByUser(User user)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                int count = context.Posts.Count(post => post.UploaderUsername.Equals(user.Username));
                return count;
            }
            catch
            {
                throw new Exception("Error counting posts");
            }
        }

        public int CountAllPost()
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                int count = context.Posts.Count();
                return count;
            }
            catch (Exception e)
            {
                throw new Exception("Count all post error");
            }
        }
        public int CountSearchPost(string searchString)
        {
            var posts = new List<Post>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                posts = context.Posts.Where(p => p.PostTitle.ToLower().Contains(searchString.ToLower())).ToList();
                int count = posts.Count();
                return count;
            }
            catch (Exception e)
            {
                throw new Exception("Count search post Error");
            }
        }
        public IEnumerable<Post> GetAllPost(int pageIndex)
        {
            List<Post> posts = new List<Post>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                SqlConnection con = (SqlConnection)context.Database.GetDbConnection();
                string SQL = "SELECT * FROM \n" +
                             "(SELECT ROW_NUMBER() OVER(ORDER BY PostId DESC) AS r, * \n" +
                             "FROM Post) as x \n" +
                             "where x.r between @index * 3 - (3 - 1) AND 3 * @index";
                SqlCommand cmd = new SqlCommand(SQL, con);
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
        //-------Checked
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
                            UploaderUsername = uploaderUsername
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
        //-------Checked
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
        //-------Checked
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
        //--------Checked
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
        //--------Checked
        public IEnumerable<Post> SearchPostByTitle(string searchString, int pageIndex)
        {
            var posts = new List<Post>();
            using var context = new PRN211_OnlyFunds_CopyContext();
            SqlConnection con = (SqlConnection)context.Database.GetDbConnection();
            string SQL = $"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY PostId DESC) AS r," +
                         $" * FROM Post WHERE PostTitle like '%{searchString}%') as x " +
                         $"WHERE x.r between @index * 3 - (3 - 1) AND 3 * @index";
            SqlCommand cmd = new SqlCommand(SQL, con);
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
                        UploaderUsername = uploaderUsername
                    };
                    posts.Add(post);
                }
                reader.NextResult();
            }

            return posts;
        }
        public int CountPostByCategory(Category category)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                IEnumerable<PostCategoryMap> postCategory =
                    context.PostCategoryMaps.Where(c => c.CategoryId == category.CategoryId).ToList();
                int count = postCategory.Count();
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public int GetMaxPostId()
        {
            int postId = 0;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext(); 
                postId = context.Posts.Max(p => p.PostId);
                if (postId ==0)
                {
                    throw new Exception("There's no post here");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return postId;
        }
        public void UpdatePost(Post editedPost)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                context.Entry<Post>(editedPost).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
