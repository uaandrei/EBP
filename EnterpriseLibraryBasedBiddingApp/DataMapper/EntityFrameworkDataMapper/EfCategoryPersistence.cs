using System;
using System.Linq;
using LoggerService;
using BiddingExceptions;
using System.Collections.Generic;
using DomainModel;

namespace DataMapper.EntityFrameworkDataMapper
{
    internal class EfCategoryPersistence : ICategoryPersistence
    {
        public void AddCategory(Category category)
        {
            try
            {
                using (var context = new BiddingDataModelContainer())
                {
                    context.Categories.AddObject(category);
                    context.SaveChanges();
                    Logger.LogInfo("Added " + category, null);
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Adding" + category + " failed!", e);
                throw new PersistenceException("Adding category failed! Check log file.", e.Message, e);
            }
        }

        public IList<Category> GetAll()
        {
                using (var context = new BiddingDataModelContainer())
                {
                    return context.Categories.ToList();
                }
        }


        public void Delete(Category category)
        {
            try
            {
                using (var context = new BiddingDataModelContainer())
                {
                    var categoryToDelete = context.Categories.FirstOrDefault(c => c.Id == category.Id);
                    if (categoryToDelete == null)
                    {
                        throw new PersistenceException("This category does not exist!", string.Empty, null);
                    }
                    context.Categories.DeleteObject(categoryToDelete);
                    context.SaveChanges();
                }
                Logger.LogInfo("Deleted " + category, null);
            }
            catch (Exception e)
            {
                Logger.LogError("Deleting failed! " + category, e);
                throw new PersistenceException("Deleting failed! Check log file.", e.Message, e);
            }
        }


        public void Update(Category category)
        {
            try
            {
                using (var context = new BiddingDataModelContainer())
                {
                    var categoryToUpdate = context.Categories.FirstOrDefault(c => c.Id == category.Id);
                    if (categoryToUpdate == null)
                    {
                        throw new PersistenceException("This category does not exist!", string.Empty, null);
                    }
                    categoryToUpdate = category;
                    context.Categories.ApplyCurrentValues(categoryToUpdate);
                    context.SaveChanges();
                }
                Logger.LogInfo("Updated " + category, null);
            }
            catch (Exception e)
            {
                Logger.LogError("Updating failed! " + category, e);
                throw new PersistenceException("Updating failed! Check log file.", e.Message, e);
            }
        }


        public void SetParentCategoryFor(Category parentCateg, Category category)
        {
            try
            {
                using (var context = new BiddingDataModelContainer())
                {
                    var categoryDb = context.Categories.FirstOrDefault(c => c.Id == category.Id);
                    var parentCategDb = context.Categories.FirstOrDefault(c => c.Id == parentCateg.Id);
                    categoryDb.ParentCategory = parentCategDb;
                    context.SaveChanges();
                }
                Logger.LogInfo("Updated " + category, null);
            }
            catch (Exception e)
            {
                Logger.LogError("Updating failed! " + category, e);
                throw new PersistenceException("Updating failed! Check log file.", e.Message, e);
            }
        }
    }
}