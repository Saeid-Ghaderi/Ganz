﻿using Ganz.Domain.Customers;
using Ganz.Domain.Orders;
using Ganz.Domain.Shared;

namespace Ganz.Domain.Managers
{
    public class OrderManager : IOrderManager
    {
        public Task CancelOrder(OrderId orderId, CustomerId customerId)
        {
            throw new NotImplementedException();
        }

        public Task CancelOrderWithAdmin(OrderId orderId, CustomerId customerId)
        {
            throw new NotImplementedException();
        }


        public Task RegisterOrder(CustomerId customerId, string address, string postalCode, string phone, List<OrderItemData> orderItems)
        {
            //steps :
            // 1- چک کردن موجودی کالاهای سفارش از انبار => با انبار کار کنیم
            // 2- ثبت سفارش و دریافت کد رهگیری سفارش
            // ? - ارسال برای پرداخت سفارش از طریق پرداخت آنلاین => سراغ سیستم پرداخت آنلاین
            // 3- کم کردن موجودی این کالا از انبار => سروقت بخش انبار
            // 4- ارسال این سفارش به کارتابل کارمندان فروش => ارسال به بخش داشبورد
            // 5- 

            return Task.FromResult(customerId);
        }
    }
}
