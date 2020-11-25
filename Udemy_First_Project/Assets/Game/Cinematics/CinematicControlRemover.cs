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
        private void Start()
        {
            // GetComponent<FakePlayableDirector>().onFinish += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = GameObject.FindWithTag("Player");


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
   
