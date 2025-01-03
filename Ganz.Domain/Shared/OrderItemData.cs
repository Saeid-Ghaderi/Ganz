﻿using Ganz.Domain.Catalogs.Products;

namespace Ganz.Domain.Shared
{
    public record class OrderItemData
    {
        public ProductId ProductId { get; init; }
        public int ItemCount { get; init; }
        public double Price { get; init; }

        public OrderItemData(ProductId productId, int itemCount, double price)
        {
            ProductId = productId;
            ItemCount = itemCount;
            Price = price;
        }
    }
}
