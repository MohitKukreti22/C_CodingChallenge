using System;

namespace LoanManagementSystem.Exceptions
{
    public class InvalidLoanException : Exception
    {
        public InvalidLoanException() : base("Invalid loan.")
        {
        }

        public InvalidLoanException(string message) : base(message)
        {
        }

        public InvalidLoanException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
