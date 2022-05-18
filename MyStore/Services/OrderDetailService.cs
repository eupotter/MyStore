using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{

    public interface IOrderDetailService
    {
        OrderDetailModel AddOrderDetail(OrderDetailModel newOrderDetail);
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<OrderDetailModel> GetAllOrderDetails();
        OrderDetailModel GetById(int id);
        void UpdateOrderDetail(OrderDetailModel model);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IMapper mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.mapper = mapper;
        }

        public IEnumerable<OrderDetailModel> GetAllOrderDetails()
        {
            //take domain objects
            var allOrderDetails = orderDetailRepository.GetAll().ToList();//List<OrderDetail>
                                                                          //transform domain objects from List<OrderDetail> -> List<OrderDetailModel>
            var orderDetailModels = mapper.Map<IEnumerable<OrderDetailModel>>(allOrderDetails);

            return orderDetailModels;
        }

        public OrderDetailModel GetById(int id)
        {
            var orderDetailToGet = orderDetailRepository.GetById(id);
            return mapper.Map<OrderDetailModel>(orderDetailToGet);
        }
        public bool Exists(int id)
        {
            return orderDetailRepository.Exists(id);
        }
        public OrderDetailModel AddOrderDetail(OrderDetailModel newOrderDetail)
        {
            OrderDetail orderDetailToAdd = mapper.Map<OrderDetail>(newOrderDetail);
            orderDetailRepository.Add(orderDetailToAdd);
            return newOrderDetail;
        }
        public void UpdateOrderDetail(OrderDetailModel model)
        {
            OrderDetail orderDetailToUpdate = mapper.Map<OrderDetail>(model);
            orderDetailRepository.Update(orderDetailToUpdate);
        }
        public bool Delete(int id)
        {
            OrderDetail itemToDelete = orderDetailRepository.GetById(id);
            return orderDetailRepository.Delete(itemToDelete);
        }
    }
}
