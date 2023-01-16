using System.Collections.Generic;
using System.IO;
using System.Linq;

public class WordsStorage
{
    public HashSet<string> Words { get; }

    private WordsStorage(string language)
    {
        var filepath = Path.Combine(Paths.DictionariesRelativePath, language);
        Words = LoadWords(filepath);
    }

    private HashSet<string> LoadWords(string filepath)
    {
        return File.ReadAllLines(filepath).ToHashSet();
    }

    public bool Exist(string word)
    {
        return Words.Contains(word);
    }
}