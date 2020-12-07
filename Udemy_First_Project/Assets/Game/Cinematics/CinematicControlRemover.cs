using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics {
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Awake()
        {
            // GetComponent<FakePlayableDirector>().onFinish += EnableControl;
           /* GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;*/
           //Changed from start to awake on refactor, added on enabl disab
            player = GameObject.FindWithTag("Player");


        }
        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }
        void DisableControl(PlayableDirector pd)
        {
            print("DisableControl ControlRemover");
            //cancel current action AND Movement(cancled in fighter Cancel()) AND disable control
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector pd)
        {

            player.GetComponent<PlayerController>().enabled = true;

            print("EnableControl ControlRemover");
        }

    }
}
   
