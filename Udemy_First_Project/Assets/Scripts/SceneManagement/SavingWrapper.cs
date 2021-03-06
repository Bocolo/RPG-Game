﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeInTime=.2f;
        const string defaultSaveFile = "save";

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }
        IEnumerator LoadLastScene()
        {
          /*  Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();*/
            //  to load last scene
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Delete();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
            //Call to savibg system
            //throw new NotImplementedException();
        }
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}
