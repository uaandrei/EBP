using DataMapper.EntityFrameworkDataMapper;
using System;

namespace DataMapper
{
    public static class DataMapperFactoryMethod
    {
        public static IDataMapperFactory GetCurrentFactory()
        {
            switch (ConfigurationConstants.Items.DataMapper)
            {
                case "ef":
                    return new EfDataMapper();
                default:
                    throw new NotImplementedException("Unimplemented data mapper: " + ConfigurationConstants.Items.DataMapper);
            }
        }
    }
}