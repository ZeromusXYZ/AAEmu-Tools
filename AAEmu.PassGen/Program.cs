using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AAEmu.PassGen
{
    class Program
    {
        static char[] passChars = new char[] {
            'a', 'b',      'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',      'm', 'n',      'p', 'q', 'r',      't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',      'J', 'K', 'L', 'M', 'N',      'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };

        static string RandomPass(int len)
        {
            string String = string.Empty;
            var Random = new Random();

            for (byte a = 0; a < len; a++)
            {
                String += passChars[Random.Next(0, passChars.Count())];
            };

            return (String);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("AAEmu.PassGen");
            Console.WriteLine("--------------");
            Console.WriteLine("This tool can be used to help generate a hashed user password for your MySQL database.");
            Console.WriteLine("It is meant to be used for the default implementation of the AAEmu servers only.");
            Console.WriteLine("If you ever want to use passwords in a serious way, it would be best to implement");
            Console.WriteLine("you own password hashing logarithm.");
            Console.WriteLine();
            Console.WriteLine("Leave the password blank to generate a random password.");
            Console.WriteLine();
            Console.Write("Password: ");
            var pass = Console.ReadLine();
            if (pass == string.Empty)
            {
                pass = RandomPass(16);
                Console.WriteLine();
                Console.WriteLine("Generated password: {0}", pass);
                Console.WriteLine();
            }
            byte[] passBytes = Encoding.UTF8.GetBytes(pass);
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(passBytes);
                var passHash = Convert.ToBase64String(hash);
                Console.WriteLine("Hashed: {0}", passHash);
            }
            Console.WriteLine();
            Console.Write("Tip: You can drag your mouse over this the result and press ENTER to copy the text to clipboard");
            Console.WriteLine();
            Console.Write("Press a key to close ...");
            Console.ReadKey();
        }
    }
}
