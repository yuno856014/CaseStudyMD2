using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBanHang
{
    class Phone
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
        public string ViewPhone()
        {
            return $"Name Product : {NameProduct}\t Price : {Price} ";
        }
    }
}
