using UnityEngine;
using DG.Tweening;
using System.Collections;
using DG.Tweening.Core;

namespace ArcAid.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TweenUIGroupFade : MonoBehaviour, ITweenSwitch
    {
        [SerializeField] bool autoEnable = true;
        [SerializeField] float duration=1;
        [SerializeField] float delay=0;
        [SerializeField] Ease ease =Ease.Linear;

        CanvasGroup cGroup;
        private void Awake()
        {
            if (cGroup == null)
                SetUp();
        }

        void SetUp()
        {
            TryGetComponent(out cGroup);
        }

        public void TweenEnable()
        {
            TweenEnable(duration);
        }
        public void TweenEnable(float duration)
        {
            TweenEnable(duration, delay);
        }

        public void TweenEnable(float duration, float delay)
        {

            if (cGroup == null)
                SetUp();

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                if (autoEnable)
                    return;
            }
            if (!gameObject.activeInHierarchy)
                return;


            cGroup.DOComplete();
            Deactivate();
            cGroup.interactable = true;
            cGroup.DOFade(1, duration).SetDelay(delay).SetEase(ease).SetUpdate(true);
        }

        private void Deactivate()
        {
            cGroup.interactable = false;
            cGroup.alpha = 0;
        }
        void OnEnable()
        {
            if (autoEnable)
            {
                TweenEnable();
            }
            else
            {
                cGroup.interactable = true;
                cGroup.alpha = 1;
            }
        }

        [ContextMenu("TweenDisable")]
        public void TweenDisable()
        {
            TweenDisable(duration);
        }
        public void TweenDisable(float duration)
        {
            if (!gameObject.activeInHierarchy)
                return;
            cGroup.interactable = false;
            cGroup.DOFade(0, duration).SetEase(ease).SetUpdate(true).onComplete = DisableNow;
        }
        void DisableNow()
        {
            if(autoEnable)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Deactivate();
        }
    }
}
