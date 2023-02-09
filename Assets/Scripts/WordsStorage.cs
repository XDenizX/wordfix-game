using System.Collections.Generic;
using System.IO;
using System.Linq;
using Constants;

public class WordsStorage
{
    public HashSet<string> Words { get; }

    public List<char> InvalidBeginnings { get; }

    private readonly string _language;

    public WordsStorage(string language)
    {
        _language = language;
        
        var filepath = Path.Combine(Paths.DictionariesRelativePath, $"{language}.txt");
        Words = LoadWords(filepath);
        InvalidBeginnings = GetInvalidBeginnings();
    }

    public bool Exist(string word)
    {
        return Words.Contains(word);
    }
    
    private HashSet<string> LoadWords(string filepath)
    {
        return File.ReadAllLines(filepath).ToHashSet();
    }

    private List<char> GetInvalidBeginnings()
    {
        var filepath = string.Format(Paths.InvalidBeginningsRelativePath, _language);
        if (File.Exists(filepath))
        {
            return GetCachedInvalidBeginnings();
        }
        
        var invalidBeginnings = FindInvalidBeginnings().ToList();
        CacheInvalidBeginnings(invalidBeginnings);

        return invalidBeginnings;
    }

    private List<char> FindInvalidBeginnings()
    {
        var allBeginnings = new List<char>();
        var allCharacters = new List<char>();
        foreach (var word in Words)
        {
            var wordBeginning = word.First();
            if (!allBeginnings.Contains(wordBeginning))
                allBeginnings.Add(wordBeginning);
                
            foreach (var character in word)
            {
                if (allCharacters.Contains(character))
                    continue;
                
                allCharacters.Add(character);
            }
        }

        return allCharacters
            .Except(allBeginnings)
            .ToList();
    }

    private void CacheInvalidBeginnings(IEnumerable<char> invalidCharacters)
    {
        var filepath = string.Format(Paths.InvalidBeginningsRelativePath, _language);
        var text = new string(invalidCharacters.ToArray());
        
        File.WriteAllText(filepath, text);
    }

    private List<char> GetCachedInvalidBeginnings()
    {
        var filepath = string.Format(Paths.InvalidBeginningsRelativePath, _language);
        var text = File.ReadAllText(filepath);

        return text.ToCharArray().ToList();
    }
}