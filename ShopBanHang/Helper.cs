using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ShopBanHang
{
    class Helper<T> where T:class
    {
        public static List<Menu> meNu = new List<Menu>();
        public static string billFile = "bill.json";
        public static T ReadFile(string filename )
        {
            var fullpath = Path.Combine(Common.FilePath, filename);
            var data = "";
            using (StreamReader sr = File.OpenText(fullpath))
            {
                data = sr.ReadToEnd();
            }   
            return JsonConvert.DeserializeObject<T>(data);
        }
        public static void WriteFile(string fileName,object data)
        {
            var serializeObject = JsonConvert.SerializeObject(data);
            var fullpath = Path.Combine(Common.FilePath, fileName);
            using(StreamWriter sw = File.AppendText(fullpath))
            {
                sw.WriteLine(serializeObject);
            }    
        }
        public static void MainMenu()
        {
            int choice = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("--------Menu--------");
                Console.WriteLine("1.Chon san pham muon mua !");
                Console.WriteLine("2.Xem gio hang !");
                Console.WriteLine("3.Xoa bot hang !");
                Console.WriteLine("0.Tinh tien va thoat !");
                Console.ResetColor();
                if(!int.TryParse(Console.ReadLine(),out choice))
                {
                    choice = 1;
                }
                Console.Clear();
                switch(choice)
                {
                    case 1:
                        muaMatHang();
                        break;
                    case 2:
                        XemGioHang();
                        break;
                    case 3:
                        break;
                    case 0:
                        TinhTien();
                        Environment.Exit(0);
                        break;
                }    
            } while (choice != 0);
        }
        public static void MuaHang(List<Menu> menus, out long sum)
        {
            var result = Helper<GioHang>.ReadFile("Menu.json");
            string choice = "";
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nhap ten hang ban muon mua !");
                Console.ResetColor();
                string name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nhap so luong ban muon mua !");
                Console.ResetColor();
                int sl = int.Parse(Console.ReadLine());
                bool check = false;
                foreach(Menu menu in menus)
                {
                    if(menu.TenSanPham.ToLower() == name.ToLower())
                    {
                        check = true;
                    }    
                }    
                for(int i = 0; i < result.Menu.Count; i++)
                {
                    if(result.Menu[i].TenSanPham.ToLower() == name.ToLower())
                    {
                        if(check)
                        {
                            foreach(Menu menu in menus)
                            {
                                if(menu.TenSanPham.ToLower() == name.ToLower())
                                {
                                    menu.SoLuong += sl;
                                }    
                            }    
                        }
                        else
                        {
                            menus.Add(new Menu()    
                            {
                                TenSanPham = name,
                                SoLuong = sl,
                                GiaTien = result.Menu[i].TongTien
                            });
                        }    
                    }    
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ban co muon tiep tuc mua hang ?");
                Console.WriteLine("Vui long An c de tiep tuc,An k de thoat!");
                Console.ResetColor();
                choice = Console.ReadLine();
            } while (choice != "k");
            sum = 0;
            foreach(Menu menu in menus)
            {
                sum += menu.TongTien;
            }    
        }
        public static void XemGioHang()
        {
            foreach(Menu menu in meNu)
            {
                Console.WriteLine(menu.ToString());
            }    
        }    
        public static void muaMatHang()
        {
            Helper<Menu>.MuaHang(meNu, out long sum);
            using (StreamWriter sw = File.CreateText(Path.Combine(Common.FilePath, billFile)))
            {
                sw.WriteLine($"Tong Bill : {sum}");
            }
        }   
        public static void TinhTien()
        {
            Helper<GioHang>.WriteFile(billFile, meNu);
        }
    }
}
