using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Resources



{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70f;
        [SerializeField] TakeDamageEvent takeDamage;
        // float healthPoints = -1f;
        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {

        }
        LazyValue<float> healthPoints;
        private bool isDead = false;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitalHealth);
        }
        private float GetInitalHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
           


           // GetComponent<BaseStats>().onLevelUp += RegerateHealth;
           /* if (healthPoints <= 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }*/
        }
        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegerateHealth;
        }
        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegerateHealth;
        }



        public bool IsDead()
        {
            return isDead;
        }
       

        public void TakeDamage(GameObject instigator,float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            
            // print(healthPoints + "  " + gameObject.name);
            if (healthPoints.value == 0)
            {
                Die();
                AwardExperience(instigator);

            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }
        public float GetHealthPoints()
        {
            return healthPoints.value;
        }
        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public float GetPercentage()
        {
            return (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100;
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
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience =instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        public object CaptureState()
        {
            return healthPoints.value;
        }
        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value == 0)
            {
                Die();

            }
        }
    }
}
