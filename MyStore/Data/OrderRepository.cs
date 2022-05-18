using Microsoft.EntityFrameworkCore;
using MyStore.Domain.Entities;
using System;
using MyStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll(string shipCountry/*, List<string> shipCities*/);
        IQueryable<Order> GetAll(List<string> shipCities, string townsStringList);
        Order GetById(int id);
        Order Add(Order newProduct);
        void Update(Order orderToUpdate);
        bool Exists(int id);
        bool Delete(Order orderToDelete);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext context;
        public OrderRepository(StoreContext context)
        {
            this.context = context;
        }
        public IQueryable<Order> GetAll(string shipCountry)
        {
            var query = this.context.Orders.Include(x => x.Cust).Select(x=>x);

            if (!string.IsNullOrEmpty(shipCountry)) 
                                                    
            {
                query = query.Where(x => x.Shipcountry == shipCountry);
            }

            return query;
        }
        public IQueryable<Order> GetAll(List<string> shipCities, string townsStringList)
        {
            var query = this.context.Orders
                //.Include(x => x.Cust)
                .Include(x=>x.OrderDetails)
                .Select(x => x);

            if (shipCities.Count!=0) 
                                                    
            {
                query = query.Where(x => shipCities.Contains(x.Shipcity));
            }
            if (!String.IsNullOrEmpty(townsStringList))
            {
                List<string> townsList = new List<string>();
                townsList = townsStringList.Split(",").ToList();
                query = query.Where(x => townsList.Contains(x.Shipcity));
            }
            //query = query.Skip(40).Take(20);sar peste 20 iteme adica 2 pagini, si afisez 20 items.

            return query;
        }
        public Order GetById(int id)
        {
            try
            {
                var result = context.Orders.FirstOrDefault(x => x.Orderid == id);
                return result;
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }
        public Order Add(Order newOrder)
        {
            var addedOrder = context.Orders.Add(newOrder);
            context.SaveChanges();
            return addedOrder.Entity;
        }
        public void Update(Order orderToUpdate)
        {
            context.Orders.Update(orderToUpdate);
            context.SaveChanges();
        }
        public bool Exists(int id)
        {
            var exists = context.Orders.Count(x => x.Orderid == id);
            return exists == 1;
        }
        public bool Delete(Order orderToDelete)
        {
            var deletedItem = context.Orders.Remove(orderToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }

    }
}
