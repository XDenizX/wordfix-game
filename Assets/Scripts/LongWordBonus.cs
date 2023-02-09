using System;

public class LongWordBonus : IBonus
{
    private const int LongWordLength = 8;
    
    public bool IsAvailable(BonusContext context)
    {
        return context.IsFullCompleted && context.Word.Length > LongWordLength;
    }

    public BonusInfo GetBonus(BonusContext context)
    {
        return new BonusInfo
        {
            Title = "Длинное слово",
            ExtraScores = 400,
            ExtraTime = TimeSpan.FromSeconds(5)
        };
    }
}