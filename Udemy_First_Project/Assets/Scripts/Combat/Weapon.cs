using RPG.Core;
using System;
using UnityEngine;
using RPG.Attributes;
using RPG.Stats;

namespace RPG.Combat
{

    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedWeaponPrefab = null;

        [SerializeField] float weaponDamageInflicted = 20f;
        [SerializeField] float percentageBonus = 0f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        const string weaponName = "Weapon";
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if (equippedWeaponPrefab!= null)
            {
                // if (isRightHanded)
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(equippedWeaponPrefab, handTransform);
                weapon.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            //var overriderController will be null if above is just runtimanimcont/root control

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            //this is to prevent animatorController for non overwritten weapons from taking on 
            // overriddenAnimCont from previously picked up weapons
            else if (overrideController != null)
            {
                
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
                    //go to root controller
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;
            oldWeapon.name = "DESTROYING"; //This prevents bug from order/frame issue 
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) { handTransform = rightHand; }
            else { handTransform = leftHand; }

            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }
        public float GetDamage()
        {
            return weaponDamageInflicted;
        }
        public float GetPercentageBonus()
        {
            return percentageBonus;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }
      /*  public void EquipWeapon()
        {

        }*/
    }
}