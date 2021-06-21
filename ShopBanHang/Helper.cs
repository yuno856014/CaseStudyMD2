using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections;

namespace ShopBanHang
{
    class Helper<T> where T : class
    {
        public static List<Phone> phone = new List<Phone>();
        public static List<Laptop> lapTop = new List<Laptop>();
        public static List<GioHang> gioHang = new List<GioHang>();
        public static BillAll billall = new BillAll();
        public static string billFile = "bill.json";
        public static long totalLaptop = 0;
        public static long totalPhone = 0;
        public static T ReadFile(string filename)
        {
            var fullpath = Path.Combine(Common.FilePath, filename);
            var data = "";
            using (StreamReader sr = File.OpenText(fullpath))
            {
                data = sr.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(data);
        }
        public static void WriteFile(string fileName, object data)
        {
            var serializeObject = JsonConvert.SerializeObject(data);
            var fullpath = Path.Combine(Common.FilePath, fileName);
            using (StreamWriter sw = File.CreateText(fullpath))
            {
                sw.WriteLine(serializeObject);
            }
        }
        public static void BuildMenu()
        {
        Main:
            int choice = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("________________________________");
                Console.WriteLine("|1: Buy Phone                  |");
                Console.WriteLine("|2: Buy Laptop                 |");
                Console.WriteLine("|0: Exit Menu                  |");
                Console.WriteLine("|______________________________|");
                Console.Write("Your choice:");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = 1;
                }
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("-------Phone-------");
                            Console.WriteLine("1.Add to cart !");
                            Console.WriteLine("2.View cart !");
                            Console.WriteLine("3.Update Amout Phone !");
                            Console.WriteLine("0.Back Menu !");
                            Console.ResetColor();
                            int choice1 = 0;
                            if (!int.TryParse(Console.ReadLine(), out choice1))
                            {
                                choice1 = 1;
                            }
                            Console.Clear();
                            switch (choice1)
                            {
                                case 1:
                                    Phone.ShowPhone();
                                   buyPhone();
                                    break;
                                case 2:
                                    VeiwPhone();
                                    break;
                                case 3:
                                    UpdatePhone();
                                    break;
                                case 0:
                                    goto Main;
                                default:
                                    Environment.Exit(0);
                                    break;
                            }
                        } while (true);
                    case 2:
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("-------Laptop-------");
                            Console.WriteLine("1.Add to cart !");
                            Console.WriteLine("2.View cart !");
                            Console.WriteLine("3.Update Amout Laptop !");
                            Console.WriteLine("0.Back Menu !");
                            Console.ResetColor();
                            int choice2 = 0;
                            if (!int.TryParse(Console.ReadLine(), out choice2))
                            {
                                choice2 = 1;
                            }
                            Console.Clear();
                            switch (choice2)
                            {
                                case 1:
                                    Laptop.ShowLatop();
                                    buyLaptop();
                                    break;
                                case 2:
                                    VeiwLaptop();
                                    break;
                                case 3:
                                    UpdateLaptop();
                                    break;
                                case 0:
                                    goto Main;
                                default:
                                    Environment.Exit(0);
                                    break;
                            }
                        } while (true);
                    case 0:
                        WriteBill();
                        break;
                }
            } while (choice != 0);
        }
        //Star buy phone
        public static void BuyPhone(List<Phone> phones, out long sumPhone)
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
                foreach (Phone phone in phones)
                {
                    if (phone.NameProduct.ToLower() == name.ToLower())
                    {
                        check = true;
                    }
                }
                for (int i = 0; i < result.Phone.Count; i++)
                {
                    if (result.Phone[i].NameProduct.ToLower() == name.ToLower())
                    {
                        if (result.Phone[i].Amount < sl)
                        {
                            Console.WriteLine("Enter too much, please enter less !");
                        }
                        else
                        {
                            if (check)
                            {
                                foreach (Phone phone in phones)
                                {
                                    if (phone.NameProduct.ToLower() == name.ToLower())
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
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Do you want to continue shopping?");
                Console.WriteLine("Please press 1 to continue!");
                Console.WriteLine("Press 2 to exit!");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = 1;
                }
            } while (choice != 2);
            sumPhone = 0;
            foreach (Phone item in phones)
            {
                sumPhone += item.TotalMoney;
            }
        }
        public static void VeiwPhone()
        {
            foreach (Phone item in phone)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public static void UpdatePhone()
        {
            Phone.ShowCart(phone);
            Console.WriteLine("Enter name update !");
            string name = Console.ReadLine();
            bool check = false;
            foreach(Phone item in phone)
            {
                if(item.NameProduct.ToLower() == name.ToLower())
                {
                    Console.WriteLine("Enter new amout !");
                    int sl = int.Parse(Console.ReadLine());
                    check = true;
                    if(sl == 0)
                    {
                        phone.Remove(item);
                        Console.WriteLine("The product has been remove! ");
                        break;
                    }
                    else
                    {
                        item.Amount = sl;
                    } 
                        
                }    
            }  
            if(check == false )
            {
                Console.WriteLine("Name does not exist");
            }
            Phone.ShowCart(phone);
        }
        public static void buyPhone()
        { 
            Helper<GioHang>.BuyPhone(phone, out long sumPhone);
            totalPhone = sumPhone;
        }
        //End Buy Phone
        //Star Buy Laptop
        public static void BuyLaptop(List<Laptop> laptops, out long sumLap)
        {
            var result = Helper<GioHang>.ReadFile("Laptop.json");
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
                foreach (Laptop laptop in laptops)
                {
                    if (laptop.NameProduct.ToLower() == name.ToLower())
                    {
                        check = true;
                    }
                }
                for (int i = 0; i < result.LapTop.Count; i++)
                {

                    if (result.LapTop[i].NameProduct.ToLower() == name.ToLower())
                    {
                        if (result.LapTop[i].Amount < sl)
                        {
                            Console.WriteLine("Enter too much, please enter less !");
                        }
                        else
                        {
                            if (check)
                            {
                                foreach (Laptop laptop in laptops)
                                {
                                    if (laptop.NameProduct.ToLower() == name.ToLower())
                                    {
                                        laptop.Price += sl;
                                    }
                                }
                            }
                            else
                            {
                                laptops.Add(new Laptop()
                                {
                                    NameProduct = name,
                                    Amount = sl,
                                    Price = result.LapTop[i].TotalMoney
                                });
                            }
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("___________________________________");
                Console.WriteLine("|Do you want to continue shopping?|");
                Console.WriteLine("|1.Continue to buy!               |");
                Console.WriteLine("|2.Exit!                          |");
                Console.WriteLine("|_________________________________|");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = 1;
                }
            } while (choice != 2);
            sumLap = 0;
            foreach (Laptop item in laptops)
            {
                sumLap += item.TotalMoney;
            }
        }
        public static void VeiwLaptop()
        {
            foreach (Laptop item in lapTop)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public static void UpdateLaptop()
        {
            Laptop.ShowCart(lapTop);
            Console.WriteLine("Enter name update !");
            string name = Console.ReadLine();
            bool check = false;
            foreach (Laptop item in lapTop)
            {
                if (item.NameProduct.ToLower() == name.ToLower())
                {
                    Console.WriteLine("Enter new amout !");
                    int sl = int.Parse(Console.ReadLine());
                    check = true;
                    if (sl == 0)
                    {
                        lapTop.Remove(item);
                        Console.WriteLine("The product has been remove! ");
                        break;
                    }
                    else
                    {
                        item.Amount = sl;
                    }

                }
            }
            if (check == false)
            {
                Console.WriteLine("Name does not exist");
            }
            Laptop.ShowCart(lapTop);
        }    
        public static void buyLaptop()
        {
            Helper<GioHang>.BuyLaptop(lapTop, out long sumLap);
            totalLaptop = sumLap;
            
        }
        //End BuyLapTop
        public static void billAll(List<GioHang> gioHangs, out long bill)
        {
            bill = 0;
            bill = totalPhone + totalLaptop;
        }
        public static void WriteBill()
        {
            Helper<GioHang>.billAll(gioHang, out long bill);
            billall.Bill.Add(phone);
            billall.Bill.Add(lapTop);
            billall.BillProduct = bill;
            Helper<GioHang>.WriteFile(billFile, billall);
        }
    }
}
