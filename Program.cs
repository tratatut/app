using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DataProcessingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the input data (format like 'FirstName.LastName.Day/Month/Year'):");
            string input = Console.ReadLine() ?? "";
            
            if (string.IsNullOrEmpty(input) || input.Length > 40)
            {
                Console.WriteLine("Invalid input. The initial string cannot be empty or more than 40 characters.");
                return;
            }
            string delimiters = @"[,#-.(\s)+/\s]";

            string[] parts = Regex.Split(input, delimiters);

            if (parts.Length != 5)
            {
                Console.WriteLine("Invalid input format. Format should be like: FirstName.LastName.Day/Month/Year (separators: .,#/- )");
                return;
            }

            string firstName = parts[0];
            string lastName = parts[1];
            string day = parts[2];
            string month = parts[3];
            string year = parts[4];

            DateTime dateOfBirth;
            if (!DateTime.TryParseExact($"{day}/{month}/{year}", "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {
                Console.WriteLine("Invalid date of birth. Format for date of birth should be like: DD/MM/YYYY");
                return;
            }

            TimeSpan timeUntilNextBirthday = GetTimeUntilNextBirthday(dateOfBirth);

            Console.WriteLine($"First Name: {firstName}");
            Console.WriteLine($"Last name: {lastName}");
            Console.WriteLine($"Date of birth: {dateOfBirth.ToString("dddd dd MMMM yyyy")}");
            Console.WriteLine($"Number of completed years: {CalculateCompletedYears(dateOfBirth)}");
            Console.WriteLine($"Next birthday in: {timeUntilNextBirthday.Days} days, {timeUntilNextBirthday.Hours} hours, {timeUntilNextBirthday.Minutes} minutes, {timeUntilNextBirthday.Seconds} seconds");

            int todayMonth = DateTime.Today.Month;
            int todayDay =DateTime.Today.Day;
            int monthInt = int.Parse(parts[3], NumberStyles.AllowThousands, new CultureInfo("en-au"));
            int dayInt = int.Parse(parts[2], NumberStyles.AllowThousands, new CultureInfo("en-au"));

            if (todayMonth == monthInt & todayDay == dayInt)
            {
                Console.WriteLine($"Congratulation, today is your {CalculateCompletedYears(dateOfBirth)} birthday!");
                return;
            }
        }

        static int CalculateCompletedYears(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (today < dateOfBirth.AddYears(age))
            {
                age--;
            }
            return age;
        }

        static TimeSpan GetTimeUntilNextBirthday(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            DateTime nextBirthday = new DateTime(today.Year, dateOfBirth.Month, dateOfBirth.Day);
            if (nextBirthday < today)
            {
                nextBirthday = nextBirthday.AddYears(1);
            }
            return nextBirthday - today;
        }
    }
}
