using LoanManagementSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DAO
{
    public interface ILoanRepository
    {
        void ApplyLoan(Loan loan);

        decimal CalculateInterest(int loanId);

        decimal CalculateInterest(int loanId, decimal principalAmount, decimal interestRate, int loanTerm);

        void LoanStatus(int loanId);

        decimal CalculateEMI(int loanId);

        decimal CalculateEMI(int loanId, decimal principalAmount, decimal interestRate, int loanTerm);

        void LoanRepayment(int loanId, double amount);

        List<Loan> GetAllLoans();

        Loan GetLoanById(int loanId);
    }
}
