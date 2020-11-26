using RPG.Core;
using UnityEngine;
 
namespace RPG.Combat
{

    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedWeaponPrefab = null;
        
        [SerializeField] float weaponDamageInflicted = 20f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedWeaponPrefab!= null)
            {
                // if (isRightHanded)
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                Instantiate(equippedWeaponPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
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
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamageInflicted);
        }
        public float GetDamage()
        {
            return weaponDamageInflicted;
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