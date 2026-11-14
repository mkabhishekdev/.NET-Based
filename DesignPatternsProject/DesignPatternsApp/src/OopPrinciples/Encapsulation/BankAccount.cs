using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
In Encapsulation this is prevented:

account.balance = -5000; // ❌ Not allowed

Instead:

account.Deposit(500);    // ✅ Controlled access
account.Withdraw(200);   // ✅ Controlled access

In one line: Encapsulation = hide internal data and provide controlled access to it.
*/
namespace DesignPatternsApp.src.OopPrinciples.Encapsulation
{
    public class BankAccount
    {
        /*
        Encapsulation: balance is kept private, so outside code cannot modify it directly. 
        Access and modification are controlled through GetBalance(), Deposit(), and Withdraw(), which enforce
        validation rules.
        */
        private decimal balance; // hidden data, encapsulation

        public BankAccount(decimal balance)
        {
            Deposit(balance);
        }
        
        public decimal GetBalance()
        {
            return balance;
        }
        
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.");
            }
            if (amount > balance)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }
            balance -= amount;
        }
    }
}