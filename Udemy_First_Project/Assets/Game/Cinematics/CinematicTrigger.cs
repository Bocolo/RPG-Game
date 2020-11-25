using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool isTriggered = false;
       /* GameObject player;
        CapsuleCollider playerCollider;*/
        private void Start()
        {
           /* MyMethod 
            player = GameObject.FindWithTag("Player");
            playerCollider = player.GetComponent<CapsuleCollider>();*/
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!isTriggered && other.gameObject.tag =="Player") /*other==playerCollider*/
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
        }
    }
}
