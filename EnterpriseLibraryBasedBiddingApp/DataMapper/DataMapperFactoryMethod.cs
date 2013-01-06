using DataMapper.EntityFrameworkDataMapper;
using System;
using System.Configuration;

namespace DataMapper
{
    public static class DataMapperFactoryMethod
    {
        public static IDataMapperFactory GetCurrentFactory()
        {
            var dataMapperValue = ConfigurationManager.AppSettings["dataMapper"];
            switch (dataMapperValue)
            {
                case "ef":
                    return new EfDataMapper();
                default:
                    throw new NotImplementedException("Unimplemented data mapper: " + dataMapperValue);
            }
        }
    }
}