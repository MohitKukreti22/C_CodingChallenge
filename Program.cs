using LoanManagementSystem.DAO;
using LoanManagementSystem.Entity;


public class MainModule
{

    public static void Main(string[] args)
    {
        ILoanRepository services = new LoanRepositoryImpl();
        LoanRepositoryImpl loanRepository = new LoanRepositoryImpl();

        MainModule obj = new MainModule();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Loan Management System Menu:");
            Console.WriteLine("1. Apply for a Loan");
            Console.WriteLine("2. Get All Loans");
            Console.WriteLine("3. Get Loan by ID");
            Console.WriteLine("4. Make Loan Repayment");
            Console.WriteLine("5. Exit");


            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    Loan loan;
                    loan = obj.GetLoanDetails();

                    services.ApplyLoan(loan);
                    break;

                case "2":
                    services.GetAllLoans();
                    break;

                case "3":
                    Console.Write("Enter Loan ID: ");
                    if (int.TryParse(Console.ReadLine(), out int loanId))
                    {
                        Loan loan3= services.GetLoanById(loanId);
                        loanRepository.PrintLoanDetails(loan3);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Loan ID format.");
                    }
                    break;

                case "4":
                    Console.Write("Enter Loan ID for Repayment: ");
                    if (int.TryParse(Console.ReadLine(), out int repaymentLoanId))
                    {
                        loanRepository.LoanRepayment(repaymentLoanId, 1000.00);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Loan ID format.");
                    }
                    break;

                case "5":
                    exit = true;
                    break;

                default:
                    // Invalid choice
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public Loan GetLoanDetails()
    {
        Loan loan=new Loan();
        Console.Write("Enter Customer ID: ");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            Customer customer = new Customer { CustomerId = customerId };

            loan.Customer = customer;
        }
        else
        {
            Console.WriteLine("Invalid Customer ID format.");
        }

        Console.Write("Enter Principal Amount: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal principalAmount))
        {
            loan.PrincipalAmount = principalAmount;
        }
        else
        {
            Console.WriteLine("Invalid Principal Amount format.");
        }

        Console.Write("Enter Interest Rate: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal interestRate))
        {
            loan.InterestRate = interestRate;
        }
        else
        {
            Console.WriteLine("Invalid Interest Rate format.");
        }

        Console.Write("Enter Loan Term (in months): ");
        if (int.TryParse(Console.ReadLine(), out int loanTerm))
        {
            loan.LoanTerm = loanTerm;
        }
        else
        {
            Console.WriteLine("Invalid Loan Term format.");
        }

        Console.Write("Enter Loan Type (CarLoan/HomeLoan): ");
        loan.LoanType = Console.ReadLine();

        loan.LoanStatus = "Pending";
        loan.LoanId = 0;

        return loan;
    }
}