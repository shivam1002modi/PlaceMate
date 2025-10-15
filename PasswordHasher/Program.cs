using System;
using BCrypt.Net;

namespace PasswordHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            // The password you want to hash
            string passwordToHash = "password123";

            // Hash the password with BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordToHash);

            Console.WriteLine("Hashed Password: " + hashedPassword);
            Console.ReadKey(); // Keep the console window open until you press a key
        }
    }
}