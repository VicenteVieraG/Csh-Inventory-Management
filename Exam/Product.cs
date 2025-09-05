namespace Exam.Product;

internal class Product
{
    public required string Id { get; init; }
    private float _price;
    public required float Price
    {
        get => _price;
        set => _price = value < 0f ? 0f : value;
    }
    public void Deconstruct(out string id, out float price) => (id, price) = (Id, Price);
};
