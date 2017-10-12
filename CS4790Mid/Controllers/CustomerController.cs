using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS4790Mid.Models;

namespace CS4790Mid.Controllers
{
    public class CustomerController : Controller
    {
        private CreditCardDBContext db = new CreditCardDBContext();



        public ActionResult CustTrans(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer cust = db.customers.Find(id);
            CustomerTransactionsViewModel custTrans = CreditCardRepository.getCustomerAndTransactions(cust);
            if (custTrans == null)
            {
                return HttpNotFound();
            }
            
            return View(custTrans);
        }


        public ActionResult CustomerList()
        {

            return View(CreditCardRepository.getAllCustomers());
        }

        public ActionResult _TransForCustomer(int?id)
        {
            List<Transaction> transaction = db.transactions.ToList();
            return PartialView(transaction);
        }


        public ActionResult CreateTrans(int? id)
        {
            Transaction transaction = new Transaction();
            transaction.custID = Convert.ToInt32(id);
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrans([Bind(Include = "transID,transDate,transDesc,transAmt,custID")] Transaction transaction)
        {
             
            
            if (ModelState.IsValid)
            {
                db.transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }



        // GET: Customer
        public ActionResult Index()
        {
            return View(CreditCardRepository.getAllCustomers());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = CreditCardRepository.getCustomer(id); //db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "custID,emailAddress,firstName,lastName,Balance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                CreditCardRepository.addCustomer(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = CreditCardRepository.getCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "custID,emailAddress,firstName,lastName,Balance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
