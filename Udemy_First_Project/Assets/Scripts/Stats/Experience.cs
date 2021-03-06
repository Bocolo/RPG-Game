﻿
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;
        // public delegate void ExperienceGainedDelegate();
        // using Action allows you to use a void delegate .. no retrn value
        //public event ExperienceGainedDelegate onExperienceGained;
        public event Action  onExperienceGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }
        public float GetExperience()
        {
            return experiencePoints;
        }
        public object CaptureState()
        {
            return experiencePoints;
        }
        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}
