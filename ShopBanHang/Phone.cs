using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBanHang
{
    class Phone : IPhone
    {
        public string NameProduct { get; set; }
        public long Price { get; set; }
        public long Amount { get; set; }
        public long TotalMoney => total1Product();
        public long total1Product()
        {
            return Price * Amount;
        }
        public override string ToString()
        {
            return $"Name Product : {NameProduct}\tPrice : {Price}\tAmount : {Amount}";
        }
        public static void ShowPhone()
        {
            var result = Helper<GioHang>.ReadFile("Phone.json");
            foreach (var item in result.Phone)
            {
                Console.WriteLine($"Name Product : {item.NameProduct}\tPrice {item.Price}VND");
            }
        }
        public static void ShowCart(List<Phone> phones)
        {
            foreach (Phone item in phones)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
