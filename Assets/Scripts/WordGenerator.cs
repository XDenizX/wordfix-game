using System;
using System.Linq;

public class WordGenerator
{
    private readonly WordsStorage _wordsStorage;
    private readonly Random _random;
    
    public WordGenerator(WordsStorage wordsStorage)
    {
        _wordsStorage = wordsStorage;
    }
    
    public string GenerateBeginning(int charCount)
    {
        var word = TakeRandomWord();
        return word[..charCount];
    }
    
    public string GenerateEnding(int charCount)
    {
        var word = TakeRandomWord();
        return word[^charCount..];
    }

    private string TakeRandomWord()
    {
        var randomIndex = _random.Next(_wordsStorage.Words.Count);
        return _wordsStorage.Words.ElementAt(randomIndex);
    }
}