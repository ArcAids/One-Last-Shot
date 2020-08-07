//using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArcAid.UI
{
    public class TweenUIGroup : MonoBehaviour, ITweenSwitch
    {
        [SerializeField] bool autoEnable = true;
        [SerializeField] float duration = 1;
        [SerializeField] float delay = 0;
        
        List<ITweenSwitch> switches;

        private void Awake()
        {
            switches = new List<ITweenSwitch>();
            if(switches.Count==0)
            {
                foreach (var item in GetComponentsInChildren<ITweenSwitch>(true))
                {
                    if(item!=this)
                        switches.Add(item);
                }
            }
        }
        void OnEnable()
        {
            if (autoEnable)
            {
                TweenEnable();
            }
        }

        [ContextMenu("TweenDisable")]
        public void TweenDisable()
        {
            foreach (var tweenSwitch in switches)
            {
                if(tweenSwitch!=this)
                    tweenSwitch.TweenDisable(duration);
            }
            StartCoroutine(DisableNow(duration));
        }

        public void TweenDisable(float duration)
        {
            foreach (var tweenSwitch in switches)
            {
                if(tweenSwitch!=this)
                    tweenSwitch.TweenDisable(duration);
            }
            StartCoroutine(DisableNow(duration));
        }

        [ContextMenu("TweenEnable")]
        public void TweenEnable()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                if (autoEnable)
                    return;
            }
            if (!gameObject.activeInHierarchy)
                return;

            Debug.Log("Logger", gameObject);

            StopAllCoroutines();
            StartCoroutine(DelayedActivation(-1, delay));
        }

        public void TweenEnable(float duration)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                if (autoEnable)
                    return;
            }
            if (!gameObject.activeInHierarchy)
                return;

            StopAllCoroutines();
            StartCoroutine(DelayedActivation(duration, delay));
        }

        public void TweenEnable(float duration, float delay)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                if (autoEnable)
                    return;
            }
            if (!gameObject.activeInHierarchy)
                return;

            StopAllCoroutines();
            StartCoroutine(DelayedActivation(duration, delay));
        }

        IEnumerator DisableNow(float delay)
        {
            if (autoEnable)
            {
                yield return new WaitForSecondsRealtime(delay);
                gameObject.SetActive(false);
            }
        }

        IEnumerator DelayedActivation(float duration, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if(duration>0)
                foreach (var tweenSwitch in switches)
                {
                    if (tweenSwitch != this)
                    {
                            tweenSwitch.TweenEnable(duration);
                    }
                }
            else
                foreach (var tweenSwitch in switches)
                {
                    if (tweenSwitch != this)
                    {
                        tweenSwitch.TweenEnable();
                    }
                }
        }
    }
}