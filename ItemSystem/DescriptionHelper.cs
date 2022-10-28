namespace ItemSystem;

public static class DescriptionHelper
{
    /// <summary>
    /// Adjusts the provided description to be more sentence-like.
    /// </summary>
    /// <param name="description">The description to be adjusted.</param>
    /// <returns>The trimmed description starting with a capital letter and ending with a period.</returns>
    public static string Sentence(this string description)
    {
        if (String.IsNullOrWhiteSpace(description)) { return String.Empty; }
        description = description.Trim();
        return $"{char.ToUpper(description.First())}{description[1..].TrimEnd()}.";
    }
}