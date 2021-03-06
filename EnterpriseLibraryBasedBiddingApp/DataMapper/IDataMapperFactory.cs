﻿namespace DataMapper
{
    public interface IDataMapperFactory
    {
        IBidPersistence BidFactory
        {
            get;
        }

        ICategoryPersistence CategoryFactory
        {
            get;
        }

        IProductPersistence ProductFactory
        {
            get;
        }

        IUserPersistence UserFactory
        {
            get;
        }

        IUserRatingPersistence UserRatingFactory
        {
            get;
        }

        IRolesPersistence RolesPersistence { get; }
    }
}