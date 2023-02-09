using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Components;
using Interfaces;
using Models;
using UnityEngine;

namespace Managers
{
    public class BonusManager : MonoBehaviour
    {
        [SerializeField]
        private Score score;

        [SerializeField]
        private GameTimer timer;
    
        private float _successEnterInterval;
        private int _answersCount;

        private List<IBonus> _bonusList;

        private void Start()
        {
            _bonusList = GetBonuses();
        }

        private void Update()
        {
            _successEnterInterval += Time.deltaTime;
        }

        public void OnSuccessEnter(string word, bool isFullCompleted)
        {
            if (isFullCompleted)
                _answersCount++;
            else
                _answersCount = 0;

            var bonusContext = new BonusContext
            {
                Word = word,
                IsFullCompleted = isFullCompleted,
                Interval = _successEnterInterval,
                FullAnswersCount = _answersCount
            };

            GiveAvailableBonuses(bonusContext);

            _successEnterInterval = 0;
        }

        private static List<IBonus> GetBonuses()
        {
            var bonusType = typeof(IBonus);
            var assembly = Assembly.GetExecutingAssembly();
        
            return assembly.GetTypes()
                .Where(type => bonusType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                .Select(type => Activator.CreateInstance(type) as IBonus)
                .ToList();
        }
    
        private void GiveAvailableBonuses(BonusContext bonusContext)
        {
            var bonuses = _bonusList
                .Where(bonus => bonus.IsAvailable(bonusContext))
                .Select(bonus => bonus.GetBonus(bonusContext));

            foreach (var bonus in bonuses)
            {
                score.Points += bonus.ExtraScores;
                timer.AppendTime(bonus.ExtraTime);
            }
        }
    }
}