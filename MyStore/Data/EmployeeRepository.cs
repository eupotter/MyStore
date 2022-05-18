using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface IEmployeeRepository
    {
        ///data access code
        ///CRUD
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        Employee Add(Employee newEmployee);
        void Update(Employee employeeToUpdate);
        bool Exists(int id);
        bool Delete(Employee employeeToDelete);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StoreContext context;

        public EmployeeRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            try
            {
                var result = context.Employees.FirstOrDefault(x => x.Empid == id);
                return result;
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }
        public Employee Add(Employee newEmployee)
        {
            var addedEmployee = context.Employees.Add(newEmployee);
            context.SaveChanges();
            return addedEmployee.Entity;
        }
        public void Update(Employee employeeToUpdate)
        {
            context.Employees.Update(employeeToUpdate);
            context.SaveChanges();
        }
        public bool Exists(int id)
        {
            var exists = context.Employees.Count(x => x.Empid == id);
            return exists == 1;
        }
        public bool Delete(Employee employeeToDelete)
        {
            var deletedItem = context.Employees.Remove(employeeToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }
    }
}
