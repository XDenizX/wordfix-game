using System;
using Interfaces;
using Models;

namespace Bonuses
{
    public class FastAnswerBonus : IBonus
    {
        private const float FastAnswerTime = 3f;
    
        public bool IsAvailable(BonusContext context)
        {
            return context.IsFullCompleted && context.Interval < FastAnswerTime;
        }

        public BonusInfo GetBonus(BonusContext context)
        {
            return new BonusInfo
            {
                Title = "Быстрый ответ",
                ExtraScores = 800,
                ExtraTime = TimeSpan.FromSeconds(5)
            };
        }
    }
}