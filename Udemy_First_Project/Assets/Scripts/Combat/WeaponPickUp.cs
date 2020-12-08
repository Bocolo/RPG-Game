using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon =null;
        [SerializeField] float healthToRestore = 0f;
        [SerializeField] float respawnTime = 10f;
      //  Fighter fighter;
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                PickUp(other.gameObject);
                // Destroy(gameObject);
                // fighter.EquipWeapon(weapon);
            }
        }

        private void PickUp(GameObject subject)
        {
            if (weapon != null)
            { subject.GetComponent<Fighter>().EquipWeapon(weapon); }
            if (healthToRestore>0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
            StartCoroutine(HideForSeconds(respawnTime));
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

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUp(callingController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.PickUp;
        }
    }
}
