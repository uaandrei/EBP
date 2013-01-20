using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ProductAuthenticityService
    {
        public int GetSimilarityPercentage(Product productA, Product productB)
        {
            var numberOfSameWords = 0;
            var textIn = string.Empty;
            var textFrom = string.Empty;

            if (productA.Description.Length > productB.Description.Length)
            {
                textFrom = productB.Description;
                textIn = productA.Description;
            }
            else
            {
                textFrom = productA.Description;
                textIn = productB.Description;
            }

            foreach (var word in textFrom.Split(' '))
            {
                if (textIn.Contains(word))
                {
                    numberOfSameWords++;
                }
            }

            var totalWordsIn = textIn.Split(' ').Length;
            var percentage = numberOfSameWords * 100 / totalWordsIn;
            return percentage;
        }
    }
}
