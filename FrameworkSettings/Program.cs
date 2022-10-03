using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkSettings
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.Title = Properties.Settings.Default.ApplicationNameDebug;
#else
            Console.Title = Properties.Settings.Default.ApplicationName;
#endif
            var test = Properties.Settings.Default.ApplicationNameDebug;

            if (String.IsNullOrEmpty(Properties.Settings.Default.UserFullName) ||
                Properties.Settings.Default.UserAge <= 0)
            {
                Console.Write("Введите ФИО:\n-->");
                Properties.Settings.Default.UserFullName = Console.ReadLine();

                Console.Write("Введите свой возраст:\n-->");
                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    Properties.Settings.Default.UserAge = age;
                }
                else
                {
                    Properties.Settings.Default.UserAge = 0;
                }
                Properties.Settings.Default.Save();
            }
            Console.WriteLine($"ФИО: {Properties.Settings.Default.UserFullName}");
            Console.WriteLine($"Возраст: {Properties.Settings.Default.UserAge}");

            Console.ReadKey(true);
        }
    }
}
