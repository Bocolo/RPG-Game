using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources



{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70f;
        float healthPoints = -1f;
        private bool isDead = false;



        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegerateHealth;
            if (healthPoints <= 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

      

        public bool IsDead()
        {
            return isDead;
        }
       

        public void TakeDamage(GameObject instigator,float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints + "  " + gameObject.name);
            if(healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);

            }
        }
        public float GetHealthPoints()
        {
            return healthPoints;
        }
        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public float GetPercentage()
        {
            return (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100;
        }
       

        private void Die()
        {
            if (isDead == false)
            {
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
               
            
            }
            
        }
        private void RegerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health)*(regenerationPercentage/100);
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience =instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints == 0)
            {
                Die();

            }
        }
    }
}
