namespace Exam;

using System.Collections.Generic;
using Exam.Product;
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
    internal void AddStock(string name) =>
        Stocks.Add(idTrack++, new Stock() {Name = name, Products = []});
    internal bool RemoveStock(uint id) => Stocks.Remove(id);
    internal void ListStocks()
    {
        Console.WriteLine("=======Stocks=======");
        foreach (var (id, (name, _)) in Stocks)
            Console.WriteLine($"- [name]: {name} - [id]: {id}");
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
                        Console.WriteLine("- Set a Stock Name:");

                        string? stockName = Console.ReadLine();
                        while (stockName == "" || stockName == null)
                        {
                            Console.WriteLine("=====Write a Non-Empty Name for the Stock=====");
                            stockName = Console.ReadLine();
                        }
                        inventory.AddStock(stockName);
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
                    case 5: Console.Clear(); inventory.ListStocks(); break;
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