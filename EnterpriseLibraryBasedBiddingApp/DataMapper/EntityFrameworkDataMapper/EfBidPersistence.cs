namespace DataMapper.EntityFrameworkDataMapper
{
    internal class EfBidPersistence : IBidPersistence
    {
        public void AddBid(DomainModel.Bid bid)
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.Bids.Add(bid);
                context.SaveChanges();
            }
        }
    }
}