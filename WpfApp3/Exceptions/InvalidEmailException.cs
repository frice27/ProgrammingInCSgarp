using System;

namespace WpfPrac1.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("Невірна адреса електронної пошти.") { }
    }
}
