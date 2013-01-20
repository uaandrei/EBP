using DomainModel;
using System.Collections.Generic;

namespace DataMapper
{
    public interface ICategoryPersistence
    {
        void AddCategory(Category category);
        
        void Delete(Category category);

        void Update(Category category);

        IList<Category> GetAll();

        void SetParentCategoryFor(Category parentCateg, Category category);
    }
}