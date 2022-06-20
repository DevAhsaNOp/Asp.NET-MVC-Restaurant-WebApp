using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurant.Models;

namespace WebAppRestaurant.Repositories
{
    public class ItemRepositories
    {
        private devahsan_RestaurantEntities restaurantDbEntities;
        public ItemRepositories()
        {
            restaurantDbEntities = new devahsan_RestaurantEntities();
        }
        public IEnumerable<SelectListItem> GetAllItems()
        {
            var result = new List<SelectListItem>();
            result = (from obj in restaurantDbEntities.Items
                      select new SelectListItem()
                      {
                          Text = obj.ItemName,
                          Value = obj.ItemId.ToString(),
                          Selected = false
                      }).ToList();
            return result;
        }
    }
}