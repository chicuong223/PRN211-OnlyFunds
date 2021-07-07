<<<<<<< HEAD
﻿using System;
<<<<<<< HEAD
using System.Collections.Generic;
using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
=======
>>>>>>> 0e2125d (CommentRepo, BookmarkRepo)
=======
﻿using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            IPostRepository postRepository = new PostRepository();
            Console.WriteLine(postRepository.GetAllPost(1));
=======
=======
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
            //IUserRepository userRepo = new UserRepository();
            //User user = new User
            //{
            //    Username = "abc",
            //    Password = "879546231",
            //    Email = "abc@gmail.com",
            //    FirstName = "John",
            //    LastName = "Doe",
            //    AvatarUrl = "abc.jpg"
            //};
            //userRepo.AddUser(user);
            //IPostRepository postRepo = new PostRepository();
            //Post post = new Post
            //{
            //    PostId = 2,
            //    PostTitle = "Title 2",
            //    PostDescription = "Desc 2",
            //    UploadDate = DateTime.Now,
            //    FileUrl = "abc.mp3",
            //    UploaderUsername = "abc"
            //};
            //postRepo.InsertPost(post);
            //Console.WriteLine(postRepo.GetPostById(2).PostTitle);

            //postRepo.DeletePost(2);
            //foreach (Post p in postRepo.GetPostByUser(userRepo.GetUserByName("abc"), 1))
            //{
            //    Console.WriteLine(p.PostTitle);
            //}
            //Console.WriteLine(postRepo.CountPostByUser(userRepo.GetUserByName("abc")));
            //IPostCategoryMapRepository postMapRepo = new PostCategoryMapRepository();
            ////postMapRepo.AddPostMap(1, 1);
            ////postMapRepo.AddPostMap(1, 2);
            //Console.WriteLine(postMapRepo.GetPostMap(1, 2).CategoryId);
            //IUserCategoryMapRepository userMapRepo = new UserCategoryMapRepository();
            //UserCategoryMap map = new UserCategoryMap
            //{
            //    Username = "abc",
            //    CategoryId = 1
            //};
            //userMapRepo.AddUserCategoryMap(map);

            //userMapRepo.DeleteCategoryMap("abc", 1);
            //Console.WriteLine(userMapRepo.GetUserCategoryMap("abc", 1).CategoryId);
            IBookmarkRepository bmRepo = new BookmarkRepository();
            bmRepo.AddBookmark("chicuong", 1);
            //bmRepo.AddBookmark("chicuong", 2);
            Bookmark bm = bmRepo.GetBookmark("chicuong", 1);
            //bmRepo.DeleteBookmark(bm);
<<<<<<< HEAD
>>>>>>> 0e2125d (CommentRepo, BookmarkRepo)
=======
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
        }
    }
}
