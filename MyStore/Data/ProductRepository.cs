using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using MyStore.Models;
using MyStore.Services;
using System.Web.Mvc;
using System.Net;

namespace MyStore.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product Add(Product newProduct);
        Product Update(Product productToUpdate);
        bool Exists(int id);
        bool Delete(Product productToDelete);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public IEnumerable<Product> FindByCategory(int categoryId)
        {
            return context.Products.Where(x => x.Categoryid == categoryId).ToList();
        }
        public Product GetById(int id)
        {
            try
            {
                var result = context.Products.FirstOrDefault(x => x.Productid == id);
                return result;
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }
        public Product Add(Product newProduct)
        {
            var addedProduct = context.Products.Add(newProduct);
            context.SaveChanges();
            return addedProduct.Entity;
        }
        public Product Update(Product productToUpdate)
        {
            var updatedProduct = context.Products.Update(productToUpdate);
            context.SaveChanges();
            return updatedProduct.Entity;
        }
        public bool Exists(int id)
        {
            var exists = context.Products.Count(x => x.Productid == id);
            return exists == 1;
        }
        public bool Delete(Product productToDelete)
        {
            var deletedItem=context.Products.Remove(productToDelete);
            context.SaveChanges();            
            return deletedItem != null;
        }
    }
}
