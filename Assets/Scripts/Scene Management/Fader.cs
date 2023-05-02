using System.Collections;
using System.Collections.Generic;
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


        //Changes alpha level of screen to black
        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        //Fades to black over time
        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        //Returns from black to normal screen color
        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }

        //Calls fade routine if current is not null
        public Coroutine Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }
        
        //Starts fade from current alpha to target alpha
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
