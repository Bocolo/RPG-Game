using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using RPG.Stats;

namespace RPG.Stats
{
    public class PlayerExpDisplay : MonoBehaviour
    {
        Experience experience;
        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }
        private void Update()
        {
            GetComponent<Text>().text = experience.GetExperience().ToString();
        }
    }
}
