using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppRestaurant.Models;
using WebAppRestaurant.ViewModel;

namespace WebAppRestaurant.Repositories
{
    public class OrderRepository
    {
        private readonly devahsan_RestaurantEntities restaurantDBEntities;
        public OrderRepository()
        {
            restaurantDBEntities = new devahsan_RestaurantEntities();
        }

        private CustomerRepositories CustomerRepositories = new CustomerRepositories();

        public bool AddOrder(OrderViewModel orderViewModel, CustomerViewModel customerViewModel)
        {
            try
            {
                var customerId = CustomerRepositories.AddCustomer(customerViewModel);
                Order objOrder = new Order()
                {
                    CustomerId = customerId,
                    FinalTotal = orderViewModel.FinalTotal,
                    OrderDate = orderViewModel.OrderDate,
                    OrderNumber = String.Format("{0:ddmmyyyyhhmmss}", DateTime.Now),
                    PaymentTypeId = orderViewModel.PaymentTypeId,
                };
                restaurantDBEntities.Orders.Add(objOrder);
                restaurantDBEntities.SaveChanges();

                foreach (var item in orderViewModel.listOrderDetailViewModel)
                {
                    var objOrderDetails = new OrderDetail()
                    {
                        Discount = item.Discount,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity,
                        OrderId = objOrder.OrderId,
                        Total = item.Total,
                        UnitPrice = item.UnitPrice
                    };
                    restaurantDBEntities.OrderDetails.Add(objOrderDetails);
                    restaurantDBEntities.SaveChanges();

                    Transaction objTransaction = new Transaction()
                    {
                        ItemId = item.ItemId,
                        Quantity = (-1) * item.Quantity,
                        TransactionDate = orderViewModel.OrderDate,
                        TypeId = 2
                    };
                    restaurantDBEntities.Transactions.Add(objTransaction);
                    restaurantDBEntities.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}