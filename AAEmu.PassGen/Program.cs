using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AAEmu.PassGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AAEmu.PasGen");
            Console.WriteLine("-------------");
            Console.WriteLine();
            Console.WriteLine("This tool can be used to help generate a hashed user password for your MySQL database");
            Console.WriteLine();
            Console.Write("Password: ");
            var pass = Console.ReadLine();
            SHA256Managed sha = new SHA256Managed();
            sha.TransformBlock()
        }
    }
}
