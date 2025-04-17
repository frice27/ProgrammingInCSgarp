using System;

namespace WpfPrac1.Exceptions
{
    public class TooOldBirthDateException : Exception
    {
        public TooOldBirthDateException() : base("Дата народження надто стара. Людина повинна бути живою.") { }
    }
}
