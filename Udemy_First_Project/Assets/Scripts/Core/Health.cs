using UnityEngine;
using RPG.Saving;
namespace RPG.Core



{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        private bool isDead = false;

      

        public bool IsDead()
        {
            return isDead;
        }
       

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints + "  " + gameObject.name);
            if(healthPoints == 0)
            {
                Die();

            }
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
