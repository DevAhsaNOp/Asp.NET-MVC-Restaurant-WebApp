using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurant.Models;
using WebAppRestaurant.Repositories;
using WebAppRestaurant.ViewModel;

namespace WebAppRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly devahsan_RestaurantEntities restaurantDBEntities;
        public HomeController()
        {
            restaurantDBEntities = new devahsan_RestaurantEntities();
        }
        public ActionResult Index()
        {
            //CustomerRepositories objcustomerRepositories = new CustomerRepositories();
            ItemRepositories objitemRepositories = new ItemRepositories();
            PaymentTypeRepositories objpaymentTypeRepositories = new PaymentTypeRepositories();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objitemRepositories.GetAllItems(), objpaymentTypeRepositories.GetAllPaymentType());
            return View(objMultipleModels);
        }

        [HttpGet]
        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = restaurantDBEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel, CustomerViewModel objCustomerViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            bool isStatus = objOrderRepository.AddOrder(objOrderViewModel, objCustomerViewModel);
            string SuccessMessage = String.Empty;

            if (isStatus)
            {
                SuccessMessage = "Your Order Has Been Successfully Placed.";
            }
            else
            {
                SuccessMessage = "There Is Some Issue While Placing Order.";
            }
            return Json(SuccessMessage, JsonRequestBehavior.AllowGet);
        }
    }
}