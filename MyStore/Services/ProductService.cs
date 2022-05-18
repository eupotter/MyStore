using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{

    public interface IProductService
    {
        ProductModel AddProduct(ProductModel newProduct);
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<ProductModel> GetAllProducts();
        ProductModel GetById(int id);
        ProductModel UpdateProduct(ProductModel model);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            //take domain objects
            var allProducts = productRepository.GetAll().ToList();//List<Product>
                                                                  //transform domain objects from List<Product> -> List<ProductModel>
            var productModels = mapper.Map<IEnumerable<ProductModel>>(allProducts);

            return productModels;

        }

        public ProductModel GetById(int id)
        {
            var productToGet=productRepository.GetById(id);
            return mapper.Map<ProductModel>(productToGet);
        }
        public bool Exists(int id)
        {
            return productRepository.Exists(id);
        }
        public ProductModel AddProduct(ProductModel newProduct)
        {
            Product productToAdd = mapper.Map<Product>(newProduct);
            productRepository.Add(productToAdd);

            return newProduct;
        }
        public ProductModel UpdateProduct(ProductModel model)
        {
            Product productToUpdate = mapper.Map<Product>(model);
            var updatedProduct = productRepository.Update(productToUpdate);
            return mapper.Map<ProductModel>(updatedProduct);
        }
        public bool Delete(int id)
        {
            Product itemToDelete = productRepository.GetById(id);
            return productRepository.Delete(itemToDelete);
        }
    }
}
