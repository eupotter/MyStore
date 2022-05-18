using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using System;
using MyStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderModel> GetAll(string shipCountry);
        IEnumerable<OrderModel> GetAll(List<string> shipCities, string townsStringList);
        OrderModel AddOrder(OrderModel newOrder);
        bool Delete(int id);
        bool Exists(int id);
        OrderModel GetById(int id);
        void UpdateOrder(OrderModel model);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public IEnumerable<OrderModel> GetAll(string shipCountry)
        {
            var allOrders = orderRepository.GetAll(shipCountry).ToList();
            var orderModels = mapper.Map<IEnumerable<OrderModel>>(allOrders);
            return orderModels;
        }
        public IEnumerable<OrderModel> GetAll(List<string> shipCities, string townsStringList)
        {
            var allOrders = orderRepository.GetAll(shipCities,townsStringList).ToList();
            var orderModels = mapper.Map<IEnumerable<OrderModel>>(allOrders);
            return orderModels;
        }
        public OrderModel GetById(int id)
        {
            var orderToGet = orderRepository.GetById(id);
            return mapper.Map<OrderModel>(orderToGet);
        }
        public bool Exists(int id)
        {
            return orderRepository.Exists(id);
        }
        public OrderModel AddOrder(OrderModel newOrder)
        {

            Order orderToAdd = mapper.Map<Order>(newOrder);
            orderRepository.Add(orderToAdd);
            return newOrder;
        }
        public void UpdateOrder(OrderModel model)
        {
            Order orderToUpdate = mapper.Map<Order>(model);
            orderRepository.Update(orderToUpdate);
        }
        public bool Delete(int id)
        {
            Order itemToDelete = orderRepository.GetById(id);
            return orderRepository.Delete(itemToDelete);
        }
    }
}
