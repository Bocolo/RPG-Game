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
        int currentLevel=0;
        public event Action onLevelUp;
        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained+= UpdateLevel;
            }
        }
        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
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
            return progression.GetStat(stat,characterClass,GetLevel());
        }
        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
                //this is to prevent issues with execution order as currentlvl initiated at 0 
            }
            return currentLevel;
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
        /*  public float GetExperienceReward()
          {
              return 10;
          }*/
    }
}
