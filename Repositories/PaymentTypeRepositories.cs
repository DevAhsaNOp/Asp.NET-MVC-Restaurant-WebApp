using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurant.Models;

namespace WebAppRestaurant.Repositories
{
    public class PaymentTypeRepositories
    {
        private devahsan_RestaurantEntities restaurantDbEntities;
        public PaymentTypeRepositories()
        {
            restaurantDbEntities = new devahsan_RestaurantEntities();
        }
        public IEnumerable<SelectListItem> GetAllPaymentType()
        {
            var result = new List<SelectListItem>();
            result = (from obj in restaurantDbEntities.PaymentTypes
                      select new SelectListItem()
                      {
                          Text = obj.PaymentTypeName,
                          Value = obj.PaymentTypeId.ToString(),
                          Selected = false
                      }).ToList();
            return result;
        }
    }
}