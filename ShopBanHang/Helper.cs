using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace ShopBanHang
{
    class Helper<T> where T:class
    {
        public static List<Phone> phoNe = new List<Phone>();
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
                Console.WriteLine("1.Select the product you want to buy !");
                Console.WriteLine("2.View cart !");
                Console.WriteLine("3.Remove product !");
                Console.WriteLine("0.Print bill and Exit Menu !");
                Console.ResetColor();
                if(!int.TryParse(Console.ReadLine(),out choice))
                {
                    choice = 1;
                }
                Console.Clear();
                switch(choice)
                {
                    case 1:
                        buyProduct();
                        break;
                    case 2:
                        VeiwProduct();
                        break;
                    case 3:
                        //Remove();
                        break;
                    case 0:
                        Bill();
                        Environment.Exit(0);
                        break;
                }    
            } while (choice != 0);
        }
        public static void Buyproduct(List<Phone> phones, out long sum)
        {
            var result = Helper<GioHang>.ReadFile("Phone.json");
            int choice = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter the name of the item you want to buy !");
                Console.ResetColor();
                string name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter the quantity you want to buy !");
                Console.ResetColor();
                int sl = int.Parse(Console.ReadLine());
                bool check = false;
                foreach(Phone phone in phones)
                {
                    if(phone.NameProduct.ToLower() == name.ToLower())
                    {
                        check = true;
                    }    
                }    
                for(int i = 0; i < result.Phone.Count; i++)
                {
                    if(result.Phone[i].NameProduct.ToLower() == name.ToLower())
                    {
                        if(check)
                        {
                            foreach(Phone phone in phones)
                            {
                                if(phone.NameProduct.ToLower() == name.ToLower())
                                {
                                    phone.Price += sl;
                                }    
                            }    
                        }
                        else
                        {
                            phones.Add(new Phone()    
                            {
                                NameProduct = name,
                                Amount = sl,
                                Price = result.Phone[i].TotalMoney
                            });
                        }    
                    }    
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Do you want to continue shopping?");
                Console.WriteLine("Please press 1 to continue!");
                Console.WriteLine("Press 2 to exit!");
                Console.ResetColor();
                if(!int.TryParse(Console.ReadLine(),out choice))
                {
                    choice = 1;
                }    
            } while (choice != 2);
            sum = 0;
            foreach(Phone item in phones)
            {
                sum += item.TotalMoney;
            }    
        }
        public static void VeiwProduct()
        {
            foreach(Phone item in phoNe)
            {
                Console.WriteLine(item.ToString());
            }    
        }   
        //public static void Remove()
        //{
        //    var nameproduct = Helper<GioHang>.ReadFile("Phone.json");
        //    Console.WriteLine("Enter name remove ");
        //    string name = Console.ReadLine();
        //    foreach(Phone item in nameproduct.Phone)
        //    {
        //        if (item.NameProduct.ToLower() == name.ToLower())
        //        {
        //            phoNe.Remove(item);
        //        }
        //        else Console.WriteLine("Product is not food");
        //    }
        //}
        public static void buyProduct()
        {
            Helper<GioHang>.Buyproduct(phoNe, out long sum);
            using (StreamWriter sw = File.CreateText(Path.Combine(Common.FilePath, billFile)))
            {
                sw.WriteLine($"Bill : {sum} VND");
            }
            
        }   
        public static void Bill()
        {
            Helper<GioHang>.WriteFile(billFile, phoNe);
            
            Console.WriteLine("Thanks !");
        }
    }
}
