namespace NotificationManger;

internal class NameSplitter
{
    static readonly char[] separator = [' '];

    public static (string FirstName, string LastName) SplitName(string fullName)
    {
        string[] parts = fullName.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length >= 2)
        {
            return (parts[0], parts[1]);
        }
        else if (parts.Length == 1)
        {
            return (parts[0], string.Empty);
        }
        else
        {
            return (string.Empty, string.Empty);
        }
    }
}
