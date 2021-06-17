using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBanHang
{
    class Menu
    {
        public string TenSanPham { get; set; }
        public long GiaTien { get; set; }
        public long SoLuong { get; set; }
        public long TongTien => tongTien1SanPham();
        public long tongTien1SanPham()
        {
            return GiaTien * SoLuong;
        }
        public override string ToString()
        {
            return $"Ten San Pham : {TenSanPham}\tGia San Pham : {GiaTien}\tSo Luong : {SoLuong}";
        }
    }
}
