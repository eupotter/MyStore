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

    public interface ICategoryService
    {
        CategoryModel AddCategory(CategoryModel newCategory);
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<CategoryModel> GetAllCategorys();
        CategoryModel GetById(int id);
        void UpdateCategory(CategoryModel model);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public IEnumerable<CategoryModel> GetAllCategorys()
        {
            //take domain objects
            var allCategorys = categoryRepository.GetAll().ToList();//List<Category>
                                                                    //transform domain objects from List<Category> -> List<CategoryModel>
            var categoryModels = mapper.Map<IEnumerable<CategoryModel>>(allCategorys);

            return categoryModels;

        }

        public CategoryModel GetById(int id)
        {
            var categoryToGet = categoryRepository.GetById(id);
            return mapper.Map<CategoryModel>(categoryToGet);
        }
        public bool Exists(int id)
        {
            return categoryRepository.Exists(id);
        }
        public CategoryModel AddCategory(CategoryModel newCategory)
        {
            Category categoryToAdd = mapper.Map<Category>(newCategory);
            categoryRepository.Add(categoryToAdd);
            return newCategory;
        }
        public void UpdateCategory(CategoryModel model)
        {
            Category categoryToUpdate = mapper.Map<Category>(model);
            categoryRepository.Update(categoryToUpdate);
        }
        public bool Delete(int id)
        {
            Category itemToDelete = categoryRepository.GetById(id);
            return categoryRepository.Delete(itemToDelete);
        }
    }
}
