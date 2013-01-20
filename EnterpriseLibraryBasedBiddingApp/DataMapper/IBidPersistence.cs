using DomainModel;
using System.Collections.Generic;

namespace DataMapper
{
    public interface IBidPersistence
    {
        void AddBid(Bid bid);

        IList<Bid> GetAll();

        void AddBidForProduct(Bid bid, Product product);
    }
}