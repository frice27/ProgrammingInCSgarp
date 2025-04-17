using System;

namespace WpfPrac1.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAdult { get; set; }
        public bool IsBirthday { get; set; }
        public string SunSign { get; set; }
        public string ChineseSign { get; set; }

        public Person() { }

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            IsAdult = CalculateAge() >= 18;
            IsBirthday = DateTime.Today.Day == birthDate.Day && DateTime.Today.Month == birthDate.Month;
            SunSign = GetWesternZodiac();
            ChineseSign = GetChineseZodiac();
        }

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
            int year = BirthDate.Year;
            return chineseZodiacs[year % 12];
        }
    }
}
