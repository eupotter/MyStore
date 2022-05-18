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

    public interface ICustomerService
    {
        CustomerModel AddCustomer(CustomerModel newCustomer);
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<CustomerModel> GetAllCustomers();
        CustomerModel GetById(int id);
        void UpdateCustomer(CustomerModel model);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            //return customerRepository.GetAll();
            var allCustomers = customerRepository.GetAll().ToList();//List<Customer>
                                                                  //transform domain objects from List<Customer> -> List<CustomerModel>
            var customerModels = mapper.Map<IEnumerable<CustomerModel>>(allCustomers);

            return customerModels;
        }

        public CustomerModel GetById(int id)
        {
            var customerToGet = customerRepository.GetById(id);
            return mapper.Map<CustomerModel>(customerToGet);
        }
        public bool Exists(int id)
        {
            return customerRepository.Exists(id);
        }
        public CustomerModel AddCustomer(CustomerModel newCustomer)
        {
            Customer customerToAdd = mapper.Map<Customer>(newCustomer);
            var addedCustomer = customerRepository.Add(customerToAdd);
            newCustomer = mapper.Map<CustomerModel>(addedCustomer);
            return newCustomer;
        }
        public void UpdateCustomer(CustomerModel model)
        {
            Customer customerToUpdate = mapper.Map<Customer>(model);
            customerRepository.Update(customerToUpdate);
        }
        public bool Delete(int id)
        {
            Customer itemToDelete = customerRepository.GetById(id);
            return customerRepository.Delete(itemToDelete);
        }
    }
}
