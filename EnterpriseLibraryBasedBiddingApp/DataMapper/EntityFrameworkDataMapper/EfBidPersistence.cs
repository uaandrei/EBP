using System;
using System.Linq;
using DomainModel;
namespace DataMapper.EntityFrameworkDataMapper
{
    internal class EfBidPersistence : IBidPersistence
    {
        public void AddBid(DomainModel.Bid bid)
        {
            try
            {
                using (var context = new BiddingDataModelContainer())
                {
                    context.Bids.AddObject(bid);
                    context.SaveChanges();
                }
                LoggerService.Logger.LogInfo("Added bid: " + bid, null);
            }
            catch (Exception e)
            {
                throw new BiddingExceptions.PersistenceException("Adding bid failed! Check log file.", e.Message, e);
            }
        }


        public System.Collections.Generic.IList<DomainModel.Bid> GetAll()
        {
            using (var context = new BiddingDataModelContainer())
            {
                return context.Bids.ToList();
            }
        }


        public void AddBidForProduct(Bid bid, Product product)
        {
            try
            {
                using (var context = new BiddingDataModelContainer())
                {
                    var productDb = context.Products.First(p => p.Id == product.Id);
                    productDb.Bids.Add(bid);
                    context.Bids.AddObject(bid);
                    context.SaveChanges();
                }
                LoggerService.Logger.LogInfo("Added bid: for product" + product.Id, null);
            }
            catch (Exception e)
            {
                throw new BiddingExceptions.PersistenceException("Adding bid failed! Check log file.", e.Message, e);
            }
        }
    }
}