using System;
using Interfaces;
using Models;

namespace Bonuses
{
    public class ComboBonus : IBonus
    {
        public bool IsAvailable(BonusContext context)
        {
            return context.IsFullCompleted && context.FullAnswersCount > 1;
        }

        public BonusInfo GetBonus(BonusContext context)
        {
            return new BonusInfo
            {
                Title = $"Комбо x{context}",
                ExtraScores = 500,
                ExtraTime = TimeSpan.FromSeconds(3)
            };
        }
    }
}