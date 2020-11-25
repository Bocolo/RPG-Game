/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Cinematics
{
    public class FakePlayableDirector : MonoBehaviour
    {
        //this is to demonstrate how event calling works, lecture #66

        public event Action<float> onFinish;
        void Start()
        {
            Invoke("OnFinish", 3f);
        }

        void OnFinish()
        {
            onFinish(4.3f); // a variable that represents a list of differ callbacks,functions that have registered themselves
        }
    }
}*/
