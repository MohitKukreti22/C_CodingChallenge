using LoanManagementSystem.Entity;
using LoanManagementSystem.Exceptions;
using LoanManagementSystem.util;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LoanManagementSystem.DAO
{
    internal class LoanRepositoryImpl : ILoanRepository
    {
        public string connectionString;
        SqlCommand cmd = null;

        public LoanRepositoryImpl()
        {
            connectionString = DBUtil.GetConnectionString();
            cmd = new SqlCommand(connectionString);
        }

        public void ApplyLoan(Loan loan)
        {
            Console.WriteLine("Loan Details:");
            Console.WriteLine($"Customer ID: {loan.Customer.CustomerId}");
            Console.WriteLine($"Principal Amount: {loan.PrincipalAmount}");
            Console.WriteLine($"Interest Rate: {loan.InterestRate}");
            Console.WriteLine($"Loan Term: {loan.LoanTerm} months");
            Console.WriteLine($"Loan Type: {loan.LoanType}");
            Console.WriteLine($"Loan Status: Pending");

            // Ask for user confirmation before proceeding
            Console.Write("Do you want to proceed and apply for the loan? (Yes/No): ");
            string userResponse = Console.ReadLine();

            if (userResponse.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Loan (CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) VALUES (@CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)", connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
                        cmd.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                        cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                        cmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                        cmd.Parameters.AddWithValue("@LoanType", loan.LoanType);
                        cmd.Parameters.AddWithValue("@LoanStatus", "Pending");

                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine("Loan application successful! The status is pending.");
                }
            }
            else
            {
                Console.WriteLine("Loan application cancelled.");
            }
        }

        public decimal CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
            {
                throw new InvalidLoanException("Loan not found");
            }

            decimal interest = CalculateInterest(loan.LoanId,loan.PrincipalAmount,loan.InterestRate,loan.LoanTerm);
            return interest;
        }
        public decimal CalculateInterest(int loanId, decimal principalAmount, decimal interestRate, int loanTerm)
        {
            
            decimal monthlyInterestRate = interestRate / 12 / 100;

            decimal interest = principalAmount * monthlyInterestRate * loanTerm;

            return interest;
        }




        public void LoanStatus(int loanId)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
            {
                throw new InvalidLoanException("Loan not found");
            }

           
            string statusMessage = (loan.Customer.CreditScore > 650) ? "Loan Approved" : "Loan Rejected";

        
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                using (SqlCommand cmd = new SqlCommand("UPDATE Loan SET LoanStatus = @Status WHERE LoanId = @LoanId", connection))
                {
                    cmd.Parameters.AddWithValue("@LoanId", loanId);
                    cmd.Parameters.AddWithValue("@Status", statusMessage);

                    cmd.ExecuteNonQuery();
                }
            }

            
            Console.WriteLine($"Loan ID: {loanId} - {statusMessage}");
        }

        public decimal CalculateEMI(int loanId)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
            {
                throw new InvalidLoanException("Loan not found");
            }

            return CalculateEMI(loan.LoanId,loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateEMI(int loanId, decimal principalAmount, decimal interestRate, int loanTerm)
        {
            decimal monthlyInterestRate = interestRate / 12 / 100;

            
            decimal emi = principalAmount * monthlyInterestRate *
                          (decimal)(Math.Pow(1 + (double)monthlyInterestRate, loanTerm)) /
                          (decimal)(Math.Pow(1 + (double)monthlyInterestRate, loanTerm) - 1);

            return emi;
        }




        public void LoanRepayment(int loanId, double amount)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
            {
                throw new InvalidLoanException("Loan not found");
            }

            int noOfEmiToPay = CalculateNoOfEMIToPay(amount, loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);

        
            decimal emiAmount = CalculateEMI(loan.LoanId);

            if ((decimal)amount < emiAmount)
            {
                Console.WriteLine("Payment rejected. Amount is less than a single EMI.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE Loan SET NoOfEmiPaid = @NoOfEmiPaid WHERE LoanId = @LoanId", connection))
                {
                    cmd.Parameters.AddWithValue("@LoanId", loanId);
                    cmd.Parameters.AddWithValue("@NoOfEmiPaid", noOfEmiToPay);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"Payment successful. {noOfEmiToPay} EMIs paid from the amount.");
        }


        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = RetrieveAllLoansFromDatabase();

            
            foreach (Loan loan in loans)
            {
                PrintLoanDetails(loan);
            }

            return loans;
        }




















        private int CalculateNoOfEMIToPay(double amount, decimal principalAmount, decimal interestRate, int loanTerm)
        {
            decimal monthlyInterestRate = interestRate / 12 / 100;

           
            double emi = (double)((double)principalAmount * (double)monthlyInterestRate * Math.Pow(1 + (double)monthlyInterestRate, loanTerm))
                / (Math.Pow(1 + (double)monthlyInterestRate, loanTerm) - 1);

           
            int noOfEmiToPay = (int)Math.Floor(amount / emi);

            return noOfEmiToPay;
        }


        public Loan GetLoanById(int loanId)
        {
            Loan loan = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Loan WHERE LoanId = @LoanId", connection))
                {
                    cmd.Parameters.AddWithValue("@LoanId", loanId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            loan = new Loan
                            {
                                LoanId = (int)reader["LoanId"],
                                PrincipalAmount = (decimal)reader["PrincipalAmount"],
                                InterestRate = (decimal)reader["InterestRate"],
                                LoanTerm = (int)reader["LoanTerm"],
                                
                            };
                        }
                    }
                }
            }

            return loan;
        }

        private List<Loan> RetrieveAllLoansFromDatabase()
        {
            List<Loan> loans = new List<Loan>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Loan", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Loan loan = new Loan
                                {
                                    LoanId = (int)reader["LoanId"],
                                    PrincipalAmount = (decimal)reader["PrincipalAmount"],
                                    InterestRate = (decimal)reader["InterestRate"],
                                    LoanTerm = (int)reader["LoanTerm"],
                                   
                                };

                                loans.Add(loan);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while retrieving loans: {ex.Message}");
                
                throw;
            }

            return loans;
        }


        public void PrintLoanDetails(Loan loan)
        {
            
            Console.WriteLine($"Loan ID: {loan.LoanId}");
            Console.WriteLine($"Principal Amount: {loan.PrincipalAmount}");
            Console.WriteLine($"Interest Rate: {loan.InterestRate}");
            Console.WriteLine($"Loan Term: {loan.LoanTerm}");
            Console.WriteLine("---------------------------------------");
        }
    }
}
