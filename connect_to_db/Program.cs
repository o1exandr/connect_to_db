using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace connect_to_db
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection con = new SqlConnection(conStr);

            UserProvider up = new UserProvider(con);
            bool success = up.Login("mail@mail", "123456");
            Console.WriteLine("success = {0}", success);

            string mail = "mail@mail.com";
            Console.WriteLine($"\n{mail} is {EmailChecker(mail)}\n");

            string[] pass = { "Qwer!", "Qwerty1!", "123456", "qwertyu1", "Qwerty1 " };
            for (int i = 0; i < pass.Length; i++)
                //Console.WriteLine($"{pass[i],10} - {PasswordChecker(pass[i])}");
                Console.WriteLine($"{pass[i],10} - {IsValidPassword(pass[i])}");

            con.Close();
        }

        static bool EmailChecker(string email)
        {
            MailAddress mail = new MailAddress(email);
            if (mail.Address == email)
                return true;
            else
                return false;
        }

        static bool PasswordChecker(string pass)
        {
            if (pass.Length < 6)
            {
                //Console.WriteLine("Is to short!");
                return false;
            }

            bool Up = false;
            bool Low = false;
            bool Digit = false;
            bool Symb = false;

            foreach (char c in pass)
            {
                if (char.IsUpper(c))
                    Up = true;
                else
                    if (char.IsLower(c))
                    Low = true;
                else
                    if (char.IsDigit(c))
                    Digit = true;
                else
                    if (char.IsPunctuation(c))
                    Symb = true;
                //Console.WriteLine($"{Up}\t{Low}\t{Digit}\t{Symb}");
            }

            if (Up && Low && Digit && Symb)
            {
                return true;
            }
            else
            {
                //Console.WriteLine("Wrong password");
                return false;
            }




        }
        public static bool IsValidPassword(string plainText)
        {
            Regex regex = new Regex(@"^(.{0,7}|[^0-9]*|[^A-Z])$");
            Match match = regex.Match(plainText);
            return match.Success;
        }
    }
}
