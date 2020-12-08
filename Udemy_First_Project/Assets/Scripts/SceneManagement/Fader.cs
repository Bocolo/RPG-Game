

using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }

        public Coroutine Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}









/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentlyActiveFade = null;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        private void Start()
        {
           // canvasGroup = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeOutIn());
        }
       *//*  IEnumerator FadeOutIn()
        {
            //nested coroutine
            yield return FadeOut(3f);
            print("Faded out");
            yield return FadeIn(1f);
            print("Faded In");
        }*//*
       public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
        public IEnumerator FadeOut (float time)
        {
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeOutRoutine(time));
            yield return currentlyActiveFade;
        }
        private IEnumerator FadeOutRoutine(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                //alpha cant overshoot but dangerous to overshoot potential
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        private IEnumerator FadeInRoutine(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeInRoutine(time));
            yield return currentlyActiveFade;
        }
    }
}

*/