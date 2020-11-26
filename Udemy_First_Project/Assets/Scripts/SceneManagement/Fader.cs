using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeOutIn());
        }
       /*  IEnumerator FadeOutIn()
        {
            //nested coroutine
            yield return FadeOut(3f);
            print("Faded out");
            yield return FadeIn(1f);
            print("Faded In");
        }*/
       public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
        public IEnumerator FadeOut (float time)
        {
            while (canvasGroup.alpha <1)
            {
                //alpha cant overshoot but dangerous to overshoot potential
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}

