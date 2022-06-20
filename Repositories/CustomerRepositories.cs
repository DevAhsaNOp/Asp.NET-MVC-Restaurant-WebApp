using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurant.Models;
using WebAppRestaurant.ViewModel;

namespace WebAppRestaurant.Repositories
{
    public class CustomerRepositories
    {
        private devahsan_RestaurantEntities restaurantDbEntities;
        public CustomerRepositories()
        {
            restaurantDbEntities = new devahsan_RestaurantEntities();
        }
        public IEnumerable<SelectListItem> GetAllCustomers()
        {
            var result = new List<SelectListItem>();
            result = (from obj in restaurantDbEntities.Customers
                      select new SelectListItem()
                      {
                          Text = obj.CustomerName,
                          Value = obj.CustomerId.ToString(),
                          Selected = false
                      }).ToList();
            return result;
        }

        public int AddCustomer(CustomerViewModel customerViewModel)
        {
            try
            {
                Customer objCustomer = new Customer()
                {
                    CustomerName = customerViewModel.CustomerName,
                    PhoneNumber = customerViewModel.PhoneNumber
                };
                restaurantDbEntities.Customers.Add(objCustomer);
                restaurantDbEntities.SaveChanges();
                return objCustomer.CustomerId;
            }
            catch
            {
                return 0;
            }
        }
    }
}