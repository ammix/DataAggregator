﻿namespace DatabaseAccess;

public partial class NotificationsBroker
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? FinHash { get; set; }
}
