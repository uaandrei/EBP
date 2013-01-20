using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationConstants
{
    public static class Items
    {
        public static int MaxProductsInBiddingStateByUser
        {
            get
            {
                return int.Parse(ReadValue("maxProductsPosted"));
            }
        }

        public static string DataMapper
        {
            get
            {
                return ReadValue("dataMapper");
            }
        }

        public static string LoggerType
        {
            get
            {
                return ReadValue("loggerType");
            }
        }

        public static int MaxProductsInBiddingStateInCategory
        {
            get
            {
                return int.Parse(ReadValue("maxProductsInCategory"));
            }
        }

        public static int MinRating
        {
            get
            {
                return int.Parse(ReadValue("minRating"));
            }
        }

        public static int BanDays
        {
            get
            {
                return int.Parse(ReadValue("banDays"));
            }
        }

        private static string ReadValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
