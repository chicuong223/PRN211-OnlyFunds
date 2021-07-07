using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFundsWeb.Helpers
{
    public class Utilities
    {
        public static string HashPassword(String rawPassword)
        {
            byte[] salt = Encoding.ASCII.GetBytes(rawPassword);

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: rawPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                ));
            return hashedPassword;
        }

        public static string UploadFile(IFormFile file, IWebHostEnvironment env, string folderName)
        {
            if (file == null || file.Length < 0)
            {
                return "";
            }
            string wwwPath = env.WebRootPath;
            string path = Path.Combine(wwwPath, folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
