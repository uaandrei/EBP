﻿using DomainModel;

namespace DataMapper
{
    public interface IProductPersistence
    {
        void AddProduct(Product product);
    }
}