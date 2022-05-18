using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface ICategoryRepository
    {
        ///data access code
        ///CRUD
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        Category Add(Category newCategory);
        void Update(Category categoryToUpdate);
        bool Exists(int id);
        bool Delete(Category categoryToDelete);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContext context;

        public CategoryRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public IEnumerable<Category> FindByCategory(int categoryId)
        {
            return context.Categories.Where(x => x.Categoryid == categoryId).ToList();
        }
        public Category GetById(int id)
        {
            try
            {
                var result = context.Categories.FirstOrDefault(x => x.Categoryid == id);
                return result;
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }
        public Category Add(Category newCategory)
        {
            var addedCategory = context.Categories.Add(newCategory);
            context.SaveChanges();
            return addedCategory.Entity;
        }
        public void Update(Category categoryToUpdate)
        {
            context.Categories.Update(categoryToUpdate);
            context.SaveChanges();
        }
        public bool Exists(int id)
        {
            var exists = context.Categories.Count(x => x.Categoryid == id);
            return exists == 1;
        }
        public bool Delete(Category categoryToDelete)
        {
            var deletedItem = context.Categories.Remove(categoryToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }
    }
}
