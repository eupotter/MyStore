using Microsoft.EntityFrameworkCore;
using MyStore.Domain.Entities;
using MyStore.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface IReportsRepository
    {
        List<CustomerContact> GetContacts();
        List<Customer> GetCustomersWithNoOrders();
    }

    public class ReportsRepository : IReportsRepository
    {
        private readonly StoreContext storeContext;
        public ReportsRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        public List<Customer> GetCustomersWithNoOrders()
        {
            var query = storeContext.Customers
                .FromSqlRaw(@"select c.custid, c.companyname,c.contactname, c.contacttitle,c.address,c.city,
                            c.region, c.postalcode, c.country, c.phone, c.fax from Customers as c
                            left join Orders on Orders.custid = c.custid
                            where Orders.custid is null");
            return query.ToList();
        }
        public List<CustomerContact> GetContacts()
        {
            var query = storeContext.CustomerContacts
                .FromSqlRaw(@"select c.custid, c.address, c.city, c.country from Customers as c
                                left join Orders on Orders.custid = c.custid
                                where Orders.custid is null");
            var result=query.ToList();
            return result;
        }
    }
}
