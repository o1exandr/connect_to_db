using System;
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

            // пошук емейлу 
            UserProvider up = new UserProvider(con);
            string mail = "Josiane4@hotmail.com";
            bool success = up.Login(mail);
            Console.WriteLine("Founded = {0}", success);

            // перевірка емейлу на валідність
            Console.WriteLine("\n\tEmail checker");            
            Console.WriteLine($"{mail} is {EmailChecker(mail)}\n");

            // перевірка паролю на валідність
            Console.WriteLine("\tPassword checker");
            string[] pass = { "Qwer!", "Qwerty1!", "123456", "qwertyu1", "Qwerty1 " };
            for (int i = 0; i < pass.Length; i++)
                //Console.WriteLine($"{pass[i],10} - {PasswordChecker(pass[i])}");
                Console.WriteLine($"{pass[i],10} - {IsValidPassword(pass[i])}");

            con.Close();
        }

        // метод валідності емейлу
        static bool EmailChecker(string email)
        {
            MailAddress mail = new MailAddress(email);
            if (mail.Address == email)
                return true;
            else
                return false;
        }

        // метод валідності паролю за допомогою Regex
        public static bool IsValidPassword(string plainText)
        {
            Regex regex = new Regex(@"^(.{0,7}|[^0-9]*|[^A-Z])$");
            Match match = regex.Match(plainText);
            return match.Success;
        }
    }
}
