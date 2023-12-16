using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Entity
{
    public class HomeLoan : Loan
    {
        public string PropertyAddress { get; set; }
        public int PropertyValue { get; set; }

  
        public HomeLoan() { }

  
        public HomeLoan(int loanId, Customer customer, decimal principalAmount,decimal interestRate, int loanTerm, string loanStatus, string propertyAddress, int propertyValue)
            : base(loanId, customer, principalAmount, interestRate, loanTerm, "HomeLoan", loanStatus)
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

   
        public string GetPropertyAddress() => PropertyAddress;
        public void SetPropertyAddress(string propertyAddress) => PropertyAddress = propertyAddress;

        public int GetPropertyValue() => PropertyValue;
        public void SetPropertyValue(int propertyValue) => PropertyValue = propertyValue;


        public override string ToString()
        {
            return $"{base.ToString()}\nProperty Address: {PropertyAddress}\nProperty Value: {PropertyValue}";
        }
    }
}
