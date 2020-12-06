using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField] Weapon weapon =null;
        [SerializeField] float respawnTime = 10f;
      //  Fighter fighter;
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {

                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
               // Destroy(gameObject);
               // fighter.EquipWeapon(weapon);
            }
        }
        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickUp(false);
            yield return new WaitForSeconds(seconds);
            ShowPickUp(true);
        }

       

        private void ShowPickUp(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }
}
