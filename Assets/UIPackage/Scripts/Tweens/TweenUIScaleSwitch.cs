using DG.Tweening;
using UnityEngine;

namespace ArcAid.UI
{
    public class TweenUIScaleSwitch : TweenSwitch
    {
        [SerializeField] Vector3 endValue;
        Vector3 originalValue;

        protected override void SetUp()
        {
            base.SetUp();
            originalValue = transform.localScale;
        }

        protected override void Activate()
        {
            transform.localScale = endValue;
        }

        protected override void AnimateTweenDisable()
        {
            transform.localScale = endValue;
            tween=transform.DOScale(originalValue, duration);
        }

        protected override void AnimateTweenEnable()
        {
            transform.localScale = originalValue;
            tween=transform.DOScale(endValue, duration);
        }

        protected override void Deactivate()
        {
            transform.localScale = originalValue;
        }
    }
}