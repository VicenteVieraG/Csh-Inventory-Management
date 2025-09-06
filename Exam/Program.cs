namespace Exam;

using System.Collections.Generic;
using Exam.StockItem;

internal class Inventory
{
    private uint idTrack;
    internal required Dictionary<uint, Stock> Stocks { get; set; }
    private static void PrintMenu()
    {
        Console.WriteLine("~~~~~~~~~~~~~~~~~Select an Option~~~~~~~~~~~~~~~~~");
        Console.WriteLine("[1]: Add a Stock");
        Console.WriteLine("[2]: Delete a Stock");
        Console.WriteLine("[3]: Add a Product to a Stock");
        Console.WriteLine("[4]: Sell/Remove a Product from a Stock");
        Console.WriteLine("[5]: List All Stocks");
        Console.WriteLine("[6]: List All Stocks' Products");
        Console.WriteLine("[7]: Show Menu");
        Console.WriteLine("[8]: Exit");
    }
    internal void AddStock(string name, float price) =>
        Stocks.Add(idTrack++, new Stock() {Id = idTrack, Name = name, Price = price, Products = []});
    internal bool RemoveStock(uint id) => Stocks.Remove(id);
    internal string Restock(uint id) => Stocks[id].AddProduct();
    internal void SellProduct(uint stockId, uint productId) => Stocks[stockId].RemoveProduct(productId);
    internal void ListStocks()
    {
        Console.WriteLine("=======Stocks=======");
        foreach (var (id, (name, _)) in Stocks)
            Console.WriteLine($"- [id]: {id} - [name]: {name}");
    }
    internal void ListProducts()
    {
        Console.Clear();
        Console.WriteLine("=======All Products=======");
        foreach (var (_, stock) in Stocks) stock.ListProducts();
    }
    public static void Main()
    {
        Inventory inventory = new() { Stocks = [] };
        bool exit = false;

        Console.WriteLine("~~~Welcome~~~");

        do
        {
            PrintMenu();
            if (uint.TryParse(Console.ReadLine(), out uint selection))
            {
                switch (selection)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("======Add Stock======");
                            Console.WriteLine("---------Set a Stock Name---------");

                            string? stockName = Console.ReadLine();
                            while (stockName == "" || stockName == null)
                            {
                                Console.WriteLine("=====Write a Non-Empty Name for the Stock=====");
                                stockName = Console.ReadLine();
                            }

                            Console.WriteLine("---------Set a Stock Price---------");
                            string? stockPrice = Console.ReadLine();
                            while (stockPrice == "" || stockPrice == null || !float.TryParse(stockPrice, out float _) || float.IsNegative(float.Parse(stockPrice)))
                            {
                                Console.WriteLine("=====Write a Valid Non-Empty Price for the Stock's Products=====");
                                stockPrice = Console.ReadLine();
                            }

                            inventory.AddStock(stockName, float.Parse(stockPrice));
                            Console.Clear();
                            Console.WriteLine($"====={stockName} Stock Added Successfully=====");
                            break;
                    }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("=====Select a Stock id to Remove=====");
                            inventory.ListStocks();

                            string? id = Console.ReadLine();
                            while (id == null || id == "" || !uint.TryParse(id, out uint _))
                            {
                                Console.Clear();
                                Console.WriteLine("======Enter a Valid Id======");
                                inventory.ListStocks();
                                id = Console.ReadLine();
                            }

                            if (!inventory.RemoveStock(uint.Parse(id)))
                            {
                                Console.Clear();
                                Console.WriteLine($"======Error Removing Stock with id: {id}======");
                            }
                            Console.WriteLine($"=========Removed Stock with id: {id}=========");
                            break;
                    }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("===========Select a Stock's Id=========");
                            inventory.ListStocks();

                            string? id = Console.ReadLine();
                            while (id == null || id == "" || !uint.TryParse(id, out uint _))
                            {
                                Console.Clear();
                                Console.WriteLine("======Enter a Valid Id======");
                                inventory.ListStocks();
                                id = Console.ReadLine();
                            }

                            string producId = inventory.Restock(uint.Parse(id));
                            Console.Clear();
                            Console.WriteLine($"=======Product {producId} Has Been Added Successfully to Stock {id}");
                            break;
                    }
                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("============Sell a Product============");
                            Console.WriteLine("----------Select a Stock----------");
                            inventory.ListStocks();

                            string? idInput = Console.ReadLine();
                            while (idInput == null || idInput == "" || !uint.TryParse(idInput, out uint _))
                            {
                                Console.Clear();
                                Console.WriteLine("======Enter a Valid Id======");
                                inventory.ListStocks();
                                idInput = Console.ReadLine();
                            }
                            uint stockId = uint.Parse(idInput);

                            Console.WriteLine("-----------Select a Product UnitId-----------");
                            inventory.Stocks[stockId].ListProducts();

                            string? UIdInput = Console.ReadLine();
                            while (UIdInput == null || UIdInput == "" || !uint.TryParse(UIdInput, out uint _))
                            {
                                Console.Clear();
                                Console.WriteLine("======Enter a Valid UnitId======");
                                inventory.ListStocks();
                                UIdInput = Console.ReadLine();
                            }
                            uint productId = uint.Parse(UIdInput);
                            inventory.SellProduct(stockId, productId);

                            Console.Clear();
                            Console.WriteLine($"==========Product {productId} Has Been Selled==========");
                            break;
                    }
                    case 5: Console.Clear(); inventory.ListStocks(); break;
                    case 6: Console.Clear(); inventory.ListProducts(); break;
                    case 7: Console.Clear(); PrintMenu(); break;
                    case 8: exit = true; break;
                    default:
                        Console.Clear();
                        Console.WriteLine("======Select a Valid Menu Option======");
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("======Enter a Valid Menu Option Number======");
            }
        } while (!exit);
    }
};