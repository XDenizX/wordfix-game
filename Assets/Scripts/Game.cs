using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private InputLine inputLine;

    [SerializeField]
    private TextBlock beginningTextBlock;

    [SerializeField]
    private TextBlock endingTextBlock;

    [SerializeField]
    private SettingsManager settingsManager;

    [SerializeField]
    private BonusManager bonusManager;

    [SerializeField]
    private GameTimer timer;

    [SerializeField]
    private Score score;
    
    private WordGenerator _wordGenerator;
    private WordsStorage _wordsStorage;
    private List<string> _usedWordsList;
        
    private void Start()
    {
        _wordsStorage = new WordsStorage(settingsManager.Settings.DictionaryLanguage);
        _wordGenerator = new WordGenerator(_wordsStorage);
        
        inputLine.TextEntered += OnTextEntered;

        GenerateBeginningAndEnding();
    }

    private void OnDestroy()
    {
        inputLine.TextEntered -= OnTextEntered;
    }

    private void OnEnable()
    {
        _usedWordsList = new List<string>();
        timer.ResetTimer();
        timer.Run();
    }

    private void OnDisable()
    {
        timer.Stop();
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    private void OnTextEntered(object sender, string enteredText)
    {
        if (!EnsureEnteredText(enteredText))
            return;
        
        if (enteredText.EndsWith(endingTextBlock.Text))
        {
            timer.AppendTime(GameConstants.SmallTime);
            score.Points += GameConstants.SmallScore;
            bonusManager.OnSuccessEnter(enteredText, true);
            UpdateEnding();
        }
        else
        {
            timer.AppendTime(GameConstants.BigTime);
            score.Points += GameConstants.BigScore;
            bonusManager.OnSuccessEnter(enteredText, false);
        }
        
        _usedWordsList.Add(enteredText);
        inputLine.Clear();
        UpdateBeginningByEnding(enteredText);
    }

    private bool EnsureEnteredText(string enteredText)
    {
        if (!enteredText.StartsWith(beginningTextBlock.Text))
        {
            // Word doesn't start with target beginning. 
            return false;
        }

        if (_usedWordsList.Contains(enteredText))
        {
            // Word has already been used.
            return false;
        }

        if (!_wordsStorage.Exist(enteredText))
        {
            // Word doesn't exist.
            return false;
        }

        return true;
    }

    private void GenerateBeginningAndEnding()
    {
        beginningTextBlock.Text = _wordGenerator.GenerateBeginning();
        endingTextBlock.Text = _wordGenerator.GenerateEnding();
    }

    private void UpdateEnding()
    {
        endingTextBlock.Text = _wordGenerator.GenerateEnding();
    }

    private void UpdateBeginningByEnding(string word)
    {
        beginningTextBlock.Text = word
            .Last(character => !_wordsStorage.InvalidBeginnings.Contains(character))
            .ToString();
    }
}