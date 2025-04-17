using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WpfPrac1.Exceptions;

namespace WpfPrac1.Models
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        public bool IsAdult { get; }
        public bool IsBirthday { get; }
        public string SunSign { get; }
        public string ChineseSign { get; }

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            if (birthDate > DateTime.Today)
                throw new FutureBirthDateException();

            if (birthDate < DateTime.Today.AddYears(-135))
                throw new TooOldBirthDateException();

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new InvalidEmailException();

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;

            IsAdult = CalculateAge() >= 18;
            IsBirthday = DateTime.Today.Day == birthDate.Day && DateTime.Today.Month == birthDate.Month;
            SunSign = GetWesternZodiac();
            ChineseSign = GetChineseZodiac();
        }

        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, DateTime.MinValue) { }

        public Person(string firstName, string lastName, DateTime birthDate)
            : this(firstName, lastName, string.Empty, birthDate) { }

        private int CalculateAge()
        {
            var today = DateTime.Today;
            int age = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-age)) age--;
            return age;
        }

        private string GetWesternZodiac()
        {
            int day = BirthDate.Day;
            int month = BirthDate.Month;
            return month switch
            {
                1 => day <= 19 ? "Козеріг" : "Водолій",
                2 => day <= 18 ? "Водолій" : "Риби",
                3 => day <= 20 ? "Риби" : "Овен",
                4 => day <= 19 ? "Овен" : "Телець",
                5 => day <= 20 ? "Телець" : "Близнюки",
                6 => day <= 20 ? "Близнюки" : "Рак",
                7 => day <= 22 ? "Рак" : "Лев",
                8 => day <= 22 ? "Лев" : "Діва",
                9 => day <= 22 ? "Діва" : "Терези",
                10 => day <= 22 ? "Терези" : "Скорпіон",
                11 => day <= 21 ? "Скорпіон" : "Стрілець",
                12 => day <= 21 ? "Стрілець" : "Козеріг",
                _ => "Невідомо"
            };
        }

        private string GetChineseZodiac()
        {
            string[] chineseZodiacs = { "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза", "Мавпа", "Півень", "Собака", "Свиня" };

            Dictionary<int, DateTime> chineseNewYearDates = new()
            {
                { 2015, new DateTime(2015, 2, 19) },
                { 2016, new DateTime(2016, 2, 8) },
                { 2017, new DateTime(2017, 1, 28) },
                { 2018, new DateTime(2018, 2, 16) },
                { 2019, new DateTime(2019, 2, 5) },
                { 2020, new DateTime(2020, 1, 25) },
                { 2021, new DateTime(2021, 2, 12) },
                { 2022, new DateTime(2022, 2, 1) },
                { 2023, new DateTime(2023, 1, 22) },
                { 2024, new DateTime(2024, 2, 10) },
                { 2025, new DateTime(2025, 1, 29) }
            };

            int year = BirthDate.Year;

            if (chineseNewYearDates.ContainsKey(year) && BirthDate < chineseNewYearDates[year])
                year--;
            else if (!chineseNewYearDates.ContainsKey(year) && BirthDate < EstimateChineseNewYear(year))
                year--;

            return chineseZodiacs[year % 12];
        }

        private DateTime EstimateChineseNewYear(int year)
        {
            return new DateTime(year, 2, 6);
        }
    }
}
