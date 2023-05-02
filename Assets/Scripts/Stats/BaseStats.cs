using GameDevTV.Utils;
using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private bool shouldUseModifiers = false;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;

        private Experience experience;

        public event Action OnLevelUp;

        LazyValue<int> currentLevel;

        private void Awake()
        {
            experience = GetComponent<Experience>();

            currentLevel = new LazyValue<int>(CalculateLevel);
        }
        private void Start()
        {
            currentLevel.ForceInit();
        }

        private void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        //Updates character level on Level up
        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();

            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEffect();
                OnLevelUp();
            }
        }

        // Getter for stats, combining base and add modifier multiplied by percentage modifiers
        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat)/100);
        }

        //Getter for current level value
        public int GetLevel()
        {
            return currentLevel.value;
        }

        //Getter for base stats based on which stat, character class, and current level
        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        //increments modifiers for each additive boost to stat returns total
        private float GetAdditiveModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        //increments modifiers for each percentage boost to stat returns total
        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        //Calculate level based on EXP
        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            //if no EXP return set starting level
            if (experience == null) return startingLevel;

            float currentEXP = experience.GetPoints();

            //variable for second to last level
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            //increase level if EXP requirements met, for every level below max
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float EXPToLevelUP = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);

                if (EXPToLevelUP > currentEXP)
                {
                    return level;
                }
            }

            //return max level
            return penultimateLevel + 1;
        }

        //spawns visual effect
        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }
    }
}
