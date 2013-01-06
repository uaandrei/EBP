namespace DataMapper.EntityFrameworkDataMapper
{
    internal class EfCategoryPersistence : ICategoryPersistence
    {
        public void AddCategory(DomainModel.Category category)
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
    }
}