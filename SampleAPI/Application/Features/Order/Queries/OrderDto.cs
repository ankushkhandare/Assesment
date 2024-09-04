namespace SampleAPI.Application.Features.Order.Queries;

public class OrderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EntryDate { get; set; }
    public bool IsInvoiced { get; set; }
}