using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{

    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookUpTable = null;
        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookUp();
            float[] levels= lookUpTable[characterClass][stat];
            if (levels.Length < level)
            {
                return 0;
            }
            return levels[level-1];
           // return lookUpTable[characterClass][stat][level];
            /*      
             *      Keeping below for sake of review--- below could cause performance issues 
             *      depending on demands and future adding. build lookup new way with dictionaries
             *      
             *      foreach(ProgressionCharacterClass progressionClass in characterClasses)
                  {
                      if (progressionClass.characterClass != characterClass) continue;
                      foreach(ProgressionStat progressionStat  in progressionClass.stats)
                      {
                          if (progressionStat.stat != stat) continue;
                          if (progressionStat.levels.Length < level) continue;

                          return progressionStat.levels[level - 1];
                      }

                  }
                  return 0;*/
        }
        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookUp();
            float[] levels = lookUpTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookUp()
        {
            if (lookUpTable != null) return;
            lookUpTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookUpTable = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookUpTable[progressionStat.stat] = progressionStat.levels;
                }
                lookUpTable[progressionClass.characterClass] = statLookUpTable;
            }
        }

        /*
 {
          // return progressionClass.health[level - 1];
       }*/

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
           // public float[] health;
        }
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}