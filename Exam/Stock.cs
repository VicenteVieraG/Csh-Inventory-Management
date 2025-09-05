namespace Exam.StockItem;

using System.Collections.Generic;
using Exam.Product;

internal class Stock
{
    private uint idTrack;
    public required string Name { get; set; }
    public required SortedDictionary<uint, Product> Products { get; set; }
    public void AddProduct(float price) => Products.Add(idTrack++, new Product { Id = $"{Name}_{idTrack}", Price = price });
    public void RemoveProduct(uint id) => Products.Remove(id);
    public void ListProducts()
    {
        foreach (var (id, (_, price)) in Products)
        {
            Console.WriteLine($"- id: {id}");
            Console.WriteLine($"- price: {price}");
        }
    }
    public void Deconstruct(out string name, out SortedDictionary<uint, Product> products) =>
        (name, products) = (Name, Products);
};