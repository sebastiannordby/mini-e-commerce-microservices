using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Models
{
    public class ProductPurchaseStats
    {
        /// <summary>
        /// Which user has bought this product.
        /// </summary>
        public string BuyersEmailAddress { get; set; }

        /// <summary>
        /// Which product that has been bought.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Total quantity bought of this product.
        /// </summary>
        public int TotalQuantityBought { get; set; }
        
        /// <summary>
        /// How many times this product has been on an order.
        /// </summary>
        public int NumberOfTimesOrdered { get; set; }
    }
}
