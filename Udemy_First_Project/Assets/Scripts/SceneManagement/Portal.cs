using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
/// <summary>
/// if character not updating properly its because nav mesh agent update conflict
/// marked with ***
/// </summary>
/// ***using unity.engine.ai

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime=1f;
        [SerializeField] float fadeInTime=1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        enum DestinationIdentifier
            //this is to list portal and choose which one in a scene to load at
        {
            A, B, C, D, E
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
                print("Portal has been triggered by " + other.gameObject.name);
            }
        }
        private IEnumerator Transition()
        {
            if (sceneToLoad < 0) {
                Debug.LogError("Scene to load not set");
                yield break; }
            //    SceneManager.LoadScene(sceneToLoad);

            
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            //save current level
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            //   print("Scene Loaded");
            //reload current level
            wrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);//only works on gameobjects at root of scene
        }

        private static void UpdatePlayer(Portal otherPortal)
        {
            GameObject player =GameObject.FindWithTag("Player");
            //***other way
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            // dont set position tranfrom if doing this
            
            //one way to do 
            //***  player.GetComponent<NavMeshAgent>().eneable=false;
        //    player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            //***  player.GetComponent<NavMeshAgent>().eneable=true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
       

    }
}
