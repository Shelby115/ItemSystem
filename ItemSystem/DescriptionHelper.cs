namespace ItemSystem;

public static class DescriptionHelper
{
    private static readonly char[] PunctuationMarks = new char[] { '.', '!', '?' };

    /// <summary>
    /// Takes a string, trims it, capitalizes the first letter, and adds a period to the end.
    /// </summary>
    public static string Sentence(this string description, char punctuationMark = '.')
    {
        return description
            .DefaultIfNull()
            .Trim()
            .ManipulateFirstCharacter(Char.ToUpper)
            .UnpunctuateSentence()
            .PunctuateSentence(punctuationMark);
    }

    /// <summary>
    /// Takes a sentence, trims it, removes the ending punctation mark, and uncapitalizes the first letter.
    /// </summary>
    public static string Unsentence(this string description)
    {
        return description
            .DefaultIfNull()
            .Trim()
            .ManipulateFirstCharacter(Char.ToLower)
            .UnpunctuateSentence();
    }

    /// <summary>
    /// Returns the provided string unless it is null, then it returns <see cref="String.Empty"/>.
    /// </summary>
    private static string DefaultIfNull(this string description)
    {
        return description ?? String.Empty;
    }

    /// <summary>
    /// Executes the provided manipulation function on the first character of this string.
    /// </summary>
    private static string ManipulateFirstCharacter(this string description, Func<char, char> manipulation)
    {
        if (description.Length == 0) {  return description; }
        return $"{manipulation(description[0])}{description[1..]}";
    }

    /// <summary>
    /// Adds a punctation mark to the end of the sentence.
    /// Must be at least one character in length to punctuate.
    /// </summary>
    private static string PunctuateSentence(this string description, char punctuationMark = '.')
    {
        if (description.Length == 0) { return description; }
        if (PunctuationMarks.Contains(description.Last())) { return description; }
        return $"{description}{punctuationMark}";
    }

    /// <summary>
    /// Removes punctuation marks you expect to find at the end of a sentence from the entire string.
    /// </summary>
    private static string UnpunctuateSentence(this string description)
    {
        foreach (var punctuationMark in PunctuationMarks)
        {
            description = description.Replace(punctuationMark.ToString(), "");
        }
        return description;
    }
}