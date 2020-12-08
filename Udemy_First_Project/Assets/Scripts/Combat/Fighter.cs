using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using GameDevTV.Utils;
using System;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvder
    {
        
        [SerializeField] float timeBetweenAttacks = 1f;   
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;  
        [SerializeField] WeaponConfig defaultWeapon = null;
      //  [SerializeField] string defaultWeaponName = "Unarmed";

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        WeaponConfig currentWeaponConfig;//= null;
        LazyValue<Weapon> currentWeapon;

        private void Awake()
        {
            currentWeaponConfig = defaultWeapon;// 
            currentWeapon =  new LazyValue<Weapon>(SetUpDefaultWeapon);
        }

        private Weapon SetUpDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon);
            
        }

        private void Start()
        {
            // AttachWeapon(currentWeaponConfig);
            currentWeapon.ForceInit();


            //currentWeaponConfig.ForceInit();
            //  Weapon weapon = Resources.Load<Weapon>(defaultWeaponName);
          /*  if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            } */
        
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
            if (target == null) { return; }
             float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
           
            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }
            if (currentWeaponConfig.HasProjectile())
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject,damage);
            }
            else
            {

                
                target.TakeDamage(gameObject,damage);//currentWeapon.GetDamage()
            }
        }
        //nimation event
        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeaponConfig.GetWeaponRange();
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
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat== Stat.Damage)
            {
                yield return currentWeaponConfig.GetDamage();
            }
        }
        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetPercentageBonus();
            }
        }
        public void EquipWeapon(WeaponConfig weapon)
        {

            //if (weapon == null) return;
            //moved to weapon
            //     Instantiate(weaponPrefab, handTransform);
            currentWeaponConfig = weapon;
            currentWeapon.value= AttachWeapon(weapon);
            // animator.runtimeAnimatorController = weaponOverride;
        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }

        public object CaptureState()
        {
            //teporary way to store weapon
            //weapon should never be null
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = UnityEngine.Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }

      
    }
}
/*{    
       * //  [SerializeField] float damageInflicted = 20f;
        //cut and moved to weapon script
//  [SerializeField] float weaponRange = 2f;
      //  [SerializeField] GameObject weaponPrefab = null;
      //  [SerializeField] float weaponRange = 2f;
      //    [SerializeField] AnimatorOverrideController weaponOverride = null;
      //    [SerializeField] AnimatorOverrideController weaponOverride = null;
      }*/