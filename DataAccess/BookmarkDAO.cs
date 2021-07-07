﻿using BusinessObjects;
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
    public class BookmarkDAO
    {
        private static BookmarkDAO instance = null;
        private static readonly object instanceLock = new object();
        private BookmarkDAO() { }
        public static BookmarkDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new BookmarkDAO();
                }
                return instance;
            }
        }

        public void AddBookmark(int postID, string username)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                Bookmark bookmark = new Bookmark
                {
                    PostId = postID,
                    Username = username
                };
                context.Bookmarks.Add(bookmark);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                context.Bookmarks.Remove(bookmark);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Bookmark GetBookmark(int postId, string username)
        {
            Bookmark bookmark = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                bookmark = context.Bookmarks.Where(b => b.PostId == postId && b.Username.Equals(username)).Single();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return bookmark;
        }

<<<<<<< HEAD
<<<<<<< HEAD
        public IEnumerable<Post> GetPostsByBookmark(string username, int pageIndex)
=======
        public IEnumerable<Post> GetPostsByBookmark(User user, int pageIndex)
>>>>>>> 0e2125d (CommentRepo, BookmarkRepo)
=======
        public IEnumerable<Post> GetPostsByBookmark(User user, int pageIndex)
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
        {
            List<Post> posts = new List<Post>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                SqlConnection con = (SqlConnection)context.Database.GetDbConnection();
<<<<<<< HEAD
<<<<<<< HEAD
                string SQL = "SELECT PostId, PostTitle, PostDescription, FileURL, UploaderUsername, UploadDate FROM \n"
                    + "(SELECT ROW_NUMBER() OVER(ORDER BY PostId) as [row], * \n"
                    + "FROM Post WHERE PostId IN( \n"
                    + "SELECT PostId FROM Bookmark \n"
                    + "WHERE Username = @username" 
                    +" )) as x \n"
                    + "WHERE x.[row] BETWEEN @index * 3 - (3 - 1) AND 3 * @index";
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@username", username);
=======
=======
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
                string SQL = "SELECT * FROM \n"
                    + "(SELECT ROW_NUMBER() OVER(ORDER BY PostId DESC) AS r, * \n"
                    + "FROM Post WHERE UploaderUsername = @username) as x \n"
                    + "where x.r between @index * 3 - (3 - 1) AND 3 * @index";
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@username", user.Username);
<<<<<<< HEAD
>>>>>>> 0e2125d (CommentRepo, BookmarkRepo)
=======
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
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
<<<<<<< HEAD
<<<<<<< HEAD
                        int postID = reader.GetInt32(0);
                        string title = reader.GetString(1);
                        string desc = reader.GetString(2);
                        string fileURL = reader.GetString(3);
                        string uploaderUsername = reader.GetString(4);
                        DateTime date = reader.GetDateTime(5);
=======
=======
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
                        int postID = reader.GetInt32(1);
                        string title = reader.GetString(2);
                        string desc = reader.GetString(3);
                        string fileURL = reader.GetString(4);
                        string uploaderUsername = reader.GetString(5);
                        DateTime date = reader.GetDateTime(6);
<<<<<<< HEAD
>>>>>>> 0e2125d (CommentRepo, BookmarkRepo)
=======
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
                        Post post = new Post
                        {
                            PostId = postID,
                            PostTitle = title,
                            PostDescription = desc,
                            FileUrl = fileURL,
                            UploadDate = date,
<<<<<<< HEAD
<<<<<<< HEAD
                            UploaderUsername = uploaderUsername
=======
                            UploaderUsernameNavigation = user
>>>>>>> 0e2125d (CommentRepo, BookmarkRepo)
=======
                            UploaderUsernameNavigation = user
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
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
    }
}
