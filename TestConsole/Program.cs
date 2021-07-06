using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
