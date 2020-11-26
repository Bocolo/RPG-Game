using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat
{
  
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks =1f;
        [SerializeField] float damageInflicted = 20f;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        private void Start()
        {
            if (weaponPrefab != null && handTransform != null)
            { SpawnWeapon(); }
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;

            
           

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position,1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //this will trigger hit() event
                TriggerAttack();

                timeSinceLastAttack = 0;


            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");

            GetComponent<Animator>().SetTrigger("attack");
        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();

            print("You have been attacked");
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
         
        }
        //animation event -- hit()
        void Hit()
        {
            if (target == null) {return; }
            target.TakeDamage(damageInflicted);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

      
        public void Cancel()
        {
            StopAttack();

            target = null;
            GetComponent<Mover>().Cancel();

        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");

            GetComponent<Animator>().SetTrigger("stopAttack");
        }
        private void SpawnWeapon()
        {
            Instantiate(weaponPrefab, handTransform);
        }
    }
}