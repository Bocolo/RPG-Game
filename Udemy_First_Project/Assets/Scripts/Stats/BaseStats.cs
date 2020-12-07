﻿using GameDevTV.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;
        LazyValue<int> currentLevel;
        public event Action onLevelUp;
        Experience experience;
        private void Awake()
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateLevel);
        }
        private void Start()
        {
            currentLevel.ForceInit(); //= CalculateLevel();
           // Experience experience = GetComponent<Experience>();
            /*if (experience != null)
            {
                experience.onExperienceGained+= UpdateLevel;
            }*/
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
        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEffect();
                onLevelUp();
                print("LeveledUp");
            }
            

           /* if (gameObject.tag == "Player")
            {
                print(GetLevel());
            }*/
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {

            return GetBaseStat(stat) + GetAdditiveModifiers(stat)*(1+ (GetPercentageModifier(stat)/100));
        }

        

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }


        public int GetLevel()
        {
           /* if (currentLevel.value < 1)
            {
                currentLevel.value = CalculateLevel();
                //this is to prevent issues with execution order as currentlvl initiated at 0 
            }*/
            return currentLevel.value;
        }
        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null)
            {
                return startingLevel;
            }
            float currentXP = experience.GetExperience();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level =1; level<= penultimateLevel; level++)
            {
                float XPToLevelUp= progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp> currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel+1;
        }
        private float GetAdditiveModifiers(Stat stat)
        {
            if (!shouldUseModifiers) { return 0; }
            float total = 0;
            foreach(IModifierProvder provider in GetComponents<IModifierProvder>())
            {
                foreach(float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) { return 0; }
            float total = 0;
            foreach (IModifierProvder provider in GetComponents<IModifierProvder>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
        /*  public float GetExperienceReward()
          {
              return 10;
          }*/
    }
}
