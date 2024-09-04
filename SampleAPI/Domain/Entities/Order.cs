namespace SampleAPI.Entities;

public class Order
{
    public Order(string name, string description)
    {
        IsInvoiced = true;
        IsDeleted = false;
    }

    public Order()
    {
        IsInvoiced = true;
        IsDeleted = false;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EntryDate { get; set; }
    public bool IsInvoiced { get; set; }
    public bool IsDeleted { get; set; }
}