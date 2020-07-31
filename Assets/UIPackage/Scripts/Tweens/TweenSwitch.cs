using UnityEngine;
using DG.Tweening;

namespace ArcAid.UI
{
    public abstract class TweenSwitch : MonoBehaviour, ITweenSwitch
    {
        [SerializeField] bool autoEnable = true;
        [SerializeField] protected float duration=1;
        [SerializeField] float delay=0;
        [SerializeField] Ease ease =Ease.Linear;

        bool isSetup;
        protected Tween tween;

        protected abstract void AnimateTweenEnable();
        protected abstract void AnimateTweenDisable();

        protected abstract void Deactivate();
        protected abstract void Activate();

        protected virtual void SetUp(){
            isSetup=true;
        }
        protected virtual bool ValidateEnable(){

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                if (autoEnable)
                    return false;
            }
            if (!gameObject.activeInHierarchy)
                return false;

            return true;
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

            if (!isSetup)
                SetUp();

            if(!ValidateEnable())
                return;

            tween.Kill();
            Deactivate();

            AnimateTweenEnable();
            tween.SetDelay(delay);
            tween.SetEase(ease);
            tween.SetUpdate(true);

        }


        void OnEnable()
        {
            if (autoEnable)
                TweenEnable();
            else
                Activate();
            
        }
        private void OnDisable()
        {
            Deactivate();
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

            AnimateTweenDisable();
            tween.SetDelay(delay);
            tween.SetEase(ease);
            tween.SetUpdate(true);
            tween.onComplete = DisableNow;
        }
        
        void DisableNow()
        {
            Deactivate();
            if(autoEnable)
                gameObject.SetActive(false);
        }

    }
}
