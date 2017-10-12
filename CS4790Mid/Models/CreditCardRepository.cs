using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS4790Mid.Models
{
    //would finish this if it was relevant to what I was doing at the time
    public class CreditCardRepository
    {
        public static List<Customer> getAllCustomers()
        {           
            return CreditCardModel.getAllCustomers();
        }

        internal static CustomerTransactionsViewModel getCustomerAndTransactions(Customer customer)
        {
           CustomerTransactionsViewModel custTrans = CreditCardModel.getCustomerAndTransactions(customer.custID);
            foreach (var item in custTrans.transactions)
            {
                if (item != null)
                {
                    item.custID = customer.custID;
                }
            }
            return custTrans;
        }

        public static Customer getCustomer(int? id)
        {
            return CreditCardModel.getCustomer(id);
        }

        public static void addCustomer(Customer customer)
        {
            CreditCardModel.addCustomer(customer);
        }
    }



}