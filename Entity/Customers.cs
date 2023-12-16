using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }

        // Default constructor
        public Customer() { }

        // Parameterized constructor
        public Customer(int customerId, string name, string email, string phoneNumber, string address, int creditScore)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            CreditScore = creditScore;
        }

        // Getter and setter methods
        public int GetCustomerId() => CustomerId;
        public void SetCustomerId(int customerId) => CustomerId = customerId;

        public string GetName() => Name;
        public void SetName(string name) => Name = name;

        public string GetEmail() => Email;
        public void SetEmail(string email) => Email = email;

        public string GetPhoneNumber() => PhoneNumber;
        public void SetPhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;

        public string GetAddress() => Address;
        public void SetAddress(string address) => Address = address;

        public int GetCreditScore() => CreditScore;
        public void SetCreditScore(int creditScore) => CreditScore = creditScore;

        // Print all information
        public override string ToString()
        {
            return $"Customer ID: {CustomerId}\nName: {Name}\nEmail: {Email}\nPhone Number: {PhoneNumber}\n" +
                   $"Address: {Address}\nCredit Score: {CreditScore}";
        }
    }
}
