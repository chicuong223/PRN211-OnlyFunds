using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IPostRepository postRepository = new PostRepository();
            IEnumerable<Post> posts = postRepository.SearchPostsByTitle("t", 1);
            Console.WriteLine(posts.Count());
            foreach (var post in posts)
            {
                Console.WriteLine(post);
            }
            Console.ReadLine();
        }
    }
}
