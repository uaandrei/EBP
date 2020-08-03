namespace DataMapper.EntityFrameworkDataMapper
{
    internal class EfDataMapper : IDataMapperFactory
    {
        public IBidPersistence BidFactory
        {
            get
            {
                return new EfBidPersistence();
            }
        }

        public ICategoryPersistence CategoryFactory
        {
            get
            {
                return new EfCategoryPersistence();
            }
        }

        public IProductPersistence ProductFactory
        {
            get
            {
                return new EfProductPersistence();
            }
        }

        public IUserPersistence UserFactory
        {
            get
            {
                return new EfUserPersistence();
            }
        }


        public IUserRatingPersistence UserRatingFactory
        {
            get
            {
                return new EfUserRatingPersistence();
            }
        }


        public IRolesPersistence RolesPersistence
        {
            get { return new EfRolePersistence(); }
        }
    }
}