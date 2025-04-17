using System;

namespace WpfPrac1.Exceptions
{
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException() : base("Дата народження не може бути в майбутньому.") { }
    }
}
