using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBanHang
{
    class Laptop : ILapTop
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
        public static void ShowLatop()
        {
            var result = Helper<GioHang>.ReadFile("Laptop.json");
            foreach (var item in result.LapTop)
            {
                Console.WriteLine($"Name Product : {item.NameProduct}\tPrice {item.Price}VND");
            }
        }
        public static void ShowCart(List<Laptop> laptops)
        {
            foreach (Laptop item in laptops)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
