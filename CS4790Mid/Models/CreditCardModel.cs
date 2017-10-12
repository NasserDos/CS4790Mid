using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CS4790Mid.Models
{
    public class CreditCardModel
    {
        public static List<Customer> getAllCustomers()
        {
            CreditCardDBContext db = new CreditCardDBContext();
            List<Customer> customer = db.customers.ToList();
            return customer;
        }



        public static CustomerTransactionsViewModel getCustomerAndTransactions(int? id)
        {
            CreditCardDBContext db = new CreditCardDBContext();
            Customer customer = db.customers.Find(id);
            CustomerTransactionsViewModel custTrans = new CustomerTransactionsViewModel();

            custTrans.customer = db.customers.Find(id);

            var transactions = db.transactions.Where(theTrans =>
            theTrans.custID == custTrans.customer.custID
            );

            custTrans.transactions = transactions.ToList();

            return custTrans;
        }

        public static void addCustomer(Customer customer)
        {
            CreditCardDBContext db = new CreditCardDBContext();
            db.customers.Add(customer);
            db.SaveChanges();
        }

        public static Customer getCustomer(int? id)
        {
            CreditCardDBContext db = new CreditCardDBContext();
            var customer = db.customers.Find(id); 
            return customer;
        }
    }




    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int custID { get; set; }
        [EmailAddress]
        public string emailAddress { get; set; }
        [MaxLength(50)]
        public string firstName { get; set; }
        [MaxLength(50)]
        public string lastName { get; set; }
        public int Balance { get; set; }


    }


    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public int transID { get; set; }
        public DateTime transDate { get; set; }
        public string transDesc { get; set; }
        //TODO: currency
        public int transAmt { get; set; }
        public int custID { get; set; }



    }

    public class CustomerTransactionsViewModel
    {
        
       public CustomerTransactionsViewModel() {
            if(customer != null)
            foreach (var trans in transactions)
            {
                trans.custID = customer.custID;
            }
        }

        public Customer customer { get; set; }
        public List<Transaction> transactions { get; set; }
    }


    public class CreditCardDBContext : DbContext
    {
        public DbSet<Customer> customers { get; set; }
        public DbSet<Transaction> transactions { get; set; }

    }



}