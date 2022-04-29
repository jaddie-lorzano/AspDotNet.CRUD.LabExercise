using Microsoft.AspNetCore.Mvc;
using CustomerData.Models;
using CustomerData.Repositories;
using System;
using System.Linq;

namespace CustomerWeb.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            var customerList = this.customerRepository.FindAll().ToList();
            return View(customerList);
        }
        public IActionResult Details(int id)
        {
            var customer = this.customerRepository.FindByPrimaryKey(id);
            ViewData["Customer"] = customer;
            return View();

        }
        public IActionResult Delete(int id)
        {
            var customer  = this.customerRepository.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult New()
        {
            ViewData["Action"] = "New";
            return View("Form", new Customer());
        }
        public IActionResult Edit(int id)
        {
            ViewData["Action"] = "Edit";
            var customer = this.customerRepository.FindByPrimaryKey(id);
            return View("Form", customer);
        }
        public IActionResult Save(string action, Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (action.ToLower().Equals("new"))
                {
                    customerRepository.Insert(customer);
                }
                else if (action.ToLower().Equals("edit"))
                {
                    customerRepository.Update(customer);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", customer);
            }
        }
    }
}
