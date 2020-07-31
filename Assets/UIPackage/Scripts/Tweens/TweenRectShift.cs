using UnityEngine;
using DG.Tweening;
using TMPro;
using DG.Tweening.Plugins.Core;
using System.Collections;
using UnityEngine.UI;

namespace ArcAid.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class TweenRectShift : MonoBehaviour, ITweenSwitch
    {
        [SerializeField] bool autoEnable = false;
        [SerializeField] float duration = 1;
        [SerializeField] Ease ease;
        [SerializeField] RectTransform targetRect;
        [SerializeField] bool controlTargetRect = false;


        RectTransform rectTransform;
        Vector2 originalPosition;
        Vector2 sizeDelta;
        Vector2 pivot;
        Vector2 anchorMax;
        Vector2 anchorMin;
        private void Awake()
        {
            if (rectTransform == null)
                SetUp();
        }

        void SetUp()
        {
            TryGetComponent(out rectTransform);

            if (controlTargetRect)
            {
                RectTransform rectTransform;
                rectTransform = targetRect;
                targetRect = this.rectTransform;
                this.rectTransform = rectTransform;
            }

            originalPosition = rectTransform.position;
            pivot = rectTransform.pivot;
            sizeDelta = rectTransform.sizeDelta;
            anchorMin = rectTransform.anchorMin;
            anchorMax = rectTransform.anchorMax;

        }
        void OnEnable()
        {
            if (autoEnable)
            {
                TweenEnable(duration);
            }
            else
            {
                rectTransform.pivot = targetRect.pivot;
                rectTransform.sizeDelta = targetRect.sizeDelta;
                rectTransform.anchorMin = targetRect.anchorMin;
                rectTransform.anchorMax = targetRect.anchorMax;
                rectTransform.position = targetRect.position;
            }
        }

        public void TweenEnable()
        {
            TweenEnable(duration);
        }

        public void TweenEnable(float duration)
        {
            TweenEnable(duration, 0);
            
        }

        private void Deactivate()
        {
            rectTransform.pivot = pivot;
            rectTransform.sizeDelta = sizeDelta;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.position = originalPosition;
        }

        [ContextMenu("TweenDisable")]
        public void TweenDisable()
        {
            TweenDisable(duration);
        }
        public void TweenDisable(float duration)
        {
            rectTransform.ShiftTo(anchorMin, anchorMax, pivot, sizeDelta, originalPosition, duration, ease);
        }

        private void OnDisable()
        {
            if (controlTargetRect && autoEnable && rectTransform.gameObject.activeInHierarchy)
                TweenDisable();
        }
        void DisableNow()
        {
            gameObject.SetActive(false);
        }

        public void TweenEnable(float duration, float delay)
        {
            if (rectTransform == null)
                SetUp();

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                if (autoEnable)
                    return;
            }
            if (!gameObject.activeInHierarchy)
                return;


            StartCoroutine(DelayedActivate(duration, delay));
        }

        IEnumerator DelayedActivate(float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            Deactivate();
            rectTransform.ShiftTo(targetRect, duration, ease);
        }
    }

    public static class ExtensionClass
    {
        public static void ShiftTo(this RectTransform rectTransform, RectTransform target, float duration, Ease ease)
        {
            rectTransform.ShiftTo(target.anchorMin, target.anchorMax, target.pivot, target.sizeDelta, target.position, duration, ease);
        }

        public static void ShiftTo(this RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot, Vector2 sizeDelta, Vector2 position, float duration, Ease ease)
        {
            rectTransform.DOAnchorMax(anchorMax, duration).SetEase(ease).SetUpdate(true);
            rectTransform.DOAnchorMin(anchorMin, duration).SetEase(ease).SetUpdate(true);
            rectTransform.DOPivot(pivot, duration).SetEase(ease).SetUpdate(true);
            rectTransform.DOSizeDelta(sizeDelta, duration).SetEase(ease).SetUpdate(true);
            rectTransform.DOMove(position, duration).SetEase(ease).SetUpdate(true);
        }

        public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
        {
            Vector3 deltaPosition = rectTransform.pivot - pivot;    // get change in pivot
            deltaPosition.Scale(rectTransform.rect.size);           // apply sizing
            deltaPosition.Scale(rectTransform.localScale);          // apply scaling
            deltaPosition = rectTransform.rotation * deltaPosition; // apply rotation

            rectTransform.pivot = pivot;                            // change the pivot
            rectTransform.localPosition -= deltaPosition;           // reverse the position change
        }

        public static void SetAnchorX(this RectTransform rectTransform, float x)
        {
            Vector2 anchorMin = rectTransform.anchorMin;
            Vector2 anchorMax = rectTransform.anchorMax;
            x=Mathf.Clamp01(x);
            if (anchorMin.x == anchorMax.x)
            {
                float canvasScale = rectTransform.GetComponentInParent<CanvasScaler>().referenceResolution.y / Screen.height;
                float width=rectTransform.parent.GetComponent<RectTransform>().rect.width *canvasScale;
                float anchoredX = rectTransform.anchoredPosition.x;
                anchoredX += width * x;
                anchorMin.x = x;
                anchorMax.x = x;
                rectTransform.anchorMin = anchorMin;
                rectTransform.anchorMax = anchorMax;
            }
        }

        public static void SetAnchorMax(this RectTransform rectTransform, Vector2 anchorMax)
        {
            Rect position = rectTransform.rect;
            //Vector3 deltaPosition = rectTransform.anchorMin - anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.rect.Set(position.x, position.y, position.width, position.height);
        }

        public static void SetActive(this GameObject gameobject, bool value, bool tween=false)
        {
            if(tween)
            {
                ITweenSwitch[] switches=gameobject.GetComponents<ITweenSwitch>();
                if (switches!=null && switches.Length>0)
                {
                    foreach (var tweenSwitch in switches)
                    {
                        if (value)
                            tweenSwitch.TweenEnable();
                        else
                            tweenSwitch.TweenDisable();
                    }
                    return;
                }
            }

            gameobject.SetActive(value);
        }
    }
}