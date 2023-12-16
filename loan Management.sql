
create database  LoanManagementSystem;



use LoanManagementSystem




-- Customer table
CREATE TABLE Customers (
    customerId INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(255),
    email VARCHAR(255),
    phoneNumber VARCHAR(20),
    address VARCHAR(255),
    creditScore INT
);

-- Loan table
CREATE TABLE Loan (
    loanId INT PRIMARY KEY IDENTITY(1,1),
    customerId INT,
    principalAmount DECIMAL(18, 2),
    interestRate DECIMAL(5, 2),
    loanTerm INT,
    loanType NVARCHAR(50), -- Use NVARCHAR for string types in SQL Server
    loanStatus NVARCHAR(50),
    FOREIGN KEY (customerId) REFERENCES Customers(customerId)
);



-- Insert 10 values into the Customers table
INSERT INTO Customers (name, email, phoneNumber, address, creditScore)
VALUES
    ('Mohit Kukreti', 'mohitkukreti975@gmail.com', '8954163169', 'kargi chowk', 800),
    ('Jane Smith', 'jane.smith@example.com', '9876543210', '456 Oak Ave', 750),
    ('Bob Johnson', 'bob.johnson@example.com', '5678901234', '789 Pine Rd', 680),
    ('Alice Williams', 'alice.williams@example.com', '4321098765', '101 Cedar Ln', 720),
    ('Charlie Brown', 'charlie.brown@example.com', '6789012345', '202 Elm Dr', 900),
    ('Eva Davis', 'eva.davis@example.com', '3456789012', '303 Birch Blvd', 670);

-- Insert 10 values into the Loan table
INSERT INTO Loan (customerId, principalAmount, interestRate, loanTerm, loanType, loanStatus)
VALUES
    (1, 5000.00, 0.05, 12, 'CarLoan', 'Pending'),
    (2, 10000.00, 0.04, 24, 'HomeLoan', 'Pending'),
    (3, 8000.00, 0.06, 18, 'CarLoan', 'Pending'),
    (4, 12000.00, 0.03, 36, 'HomeLoan', 'Pending'),
    (5, 6000.00, 0.05, 12, 'CarLoan', 'Pending');
 