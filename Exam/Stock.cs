namespace Exam.StockItem;

using System.Collections.Generic;
using Exam.Product;

internal class Stock
{
    private uint idTrack;
    public required uint Id { get; init; }
    public required string Name { get; set; }
    private float _price;
    public required float Price
    {
        get => _price;
        set => _price = value < 0f ? 0f : value;
    }
    public required SortedDictionary<uint, Product> Products { get; set; }
    public string AddProduct()
    {
        string producId = $"{Name}_{idTrack}";
        Products.Add(idTrack++, new(producId, idTrack));

        return producId;
    }
    public void RemoveProduct(uint id) => Products.Remove(id);
    public void ListProducts()
    {
        Console.WriteLine($"----------{Name}----------");
        Console.WriteLine($"- [Stock Id]: {Id}");
        Console.WriteLine($"- [Price]: {_price}");
        foreach (var (_, product) in Products)
            Console.WriteLine($"- [Product Id]: {product.Id}");
    }
    public void Deconstruct(out string name, out SortedDictionary<uint, Product> products) =>
        (name, products) = (Name, Products);
};