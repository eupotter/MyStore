using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface IOrderDetailRepository
    {
        ///data access code
        ///CRUD
        IEnumerable<OrderDetail> GetAll();
        OrderDetail GetById(int id);
        OrderDetail Add(OrderDetail newOrderDetail);
        void Update(OrderDetail orderDetailToUpdate);
        bool Exists(int id);
        bool Delete(OrderDetail orderDetailToDelete);
    }

    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly StoreContext context;

        public OrderDetailRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return context.OrderDetails.ToList();
        }

        public IEnumerable<OrderDetail> FindByCategory(int categoryId)
        {
            return context.OrderDetails.Where(x => x.Orderid == categoryId).ToList();
        }
        public OrderDetail GetById(int id)
        {
            try
            {
                var result = context.OrderDetails.FirstOrDefault(x => x.Orderid == id);
                return result;
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }
        public OrderDetail Add(OrderDetail newOrderDetail)
        {
            var addedOrderDetail = context.OrderDetails.Add(newOrderDetail);
            context.SaveChanges();
            return addedOrderDetail.Entity;
        }
        public void Update(OrderDetail orderDetailToUpdate)
        {
            context.OrderDetails.Update(orderDetailToUpdate);
            context.SaveChanges();
        }
        public bool Exists(int id)
        {
            var exists = context.OrderDetails.Count(x => x.Orderid == id);
            return exists == 1;
        }
        public bool Delete(OrderDetail orderDetailToDelete)
        {
            var deletedItem = context.OrderDetails.Remove(orderDetailToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }
    }
}
