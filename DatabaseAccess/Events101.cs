namespace DatabaseAccess;

public partial class Events101
{
    public decimal Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime EventDate { get; set; }

    public string EventType { get; set; } = null!;
}
