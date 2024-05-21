namespace DatabaseAccess;

public partial class Events145
{
    public decimal Id { get; set; }

    public string CustomerId { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public string EventType { get; set; } = null!;
}
