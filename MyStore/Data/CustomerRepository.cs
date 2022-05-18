using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface ICustomerRepository
    {
        Customer Add(Customer newCustomer);

        ///data access code
        ///CRUD
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        void Update(Customer customerToUpdate);
        bool Exists(int id);
        bool Delete(Customer customerToDelete);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreContext context;

        public CustomerRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            try
            {
                var result = context.Customers.Where(x => x.Custid == id).First();
                return result;
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }
        public Customer Add(Customer newCustomer)
        {
            var addedCustomer = context.Customers.Add(newCustomer);
            context.SaveChanges();
            return addedCustomer.Entity;
        }
        public void Update(Customer customerToUpdate)
        {
            context.Customers.Update(customerToUpdate);
            context.SaveChanges();
        }
        public bool Exists(int id)
        {
            var exists = context.Customers.Count(x => x.Custid == id);
            return exists == 1;
        }
        public bool Delete(Customer customerToDelete)
        {
            var deletedItem=context.Customers.Remove(customerToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }
    }
}
