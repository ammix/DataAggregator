namespace NotificationManger;

internal static class ClientCodeGenerator
{
    public static string GenerateCode(string? firstName, string? lastName, string? organisation)
    {
        return $"{SkipFirstLetterAndRevertNextThree(firstName)}-{SkipFirstLetterAndRevertNextThree(lastName)}-{TakeFirstLetters(organisation)}";
    }

    static string SkipFirstLetterAndRevertNextThree(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return new string(value.Skip(1).Take(3).Reverse().ToArray()).ToUpper();
    }

    static string TakeFirstLetters(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return new string(value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                               .Select(word => word[0])
                                               .ToArray()).ToUpper();
    }
}
