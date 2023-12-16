using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Entity
{
    public class CarLoan : Loan
    {
        public string CarModel { get; set; }
        public int CarValue { get; set; }

       
        public CarLoan() { }

        
        public CarLoan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanStatus, string carModel, int carValue)
            : base(loanId, customer, principalAmount, interestRate, loanTerm, "CarLoan", loanStatus)
        {
            CarModel = carModel;
            CarValue = carValue;
        }

      
        public string GetCarModel() => CarModel;
        public void SetCarModel(string carModel) => CarModel = carModel;

        public int GetCarValue() => CarValue;
        public void SetCarValue(int carValue) => CarValue = carValue;

        
        public override string ToString()
        {
            return $"{base.ToString()}\nCar Model: {CarModel}\nCar Value: {CarValue}";
        }
    }
}
