using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
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
    }
}
