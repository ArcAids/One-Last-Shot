using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

namespace ArcAid.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class TweenUISlideIn : MonoBehaviour, ITweenSwitch
    {
        [SerializeField] bool autoEnable = true;
        [SerializeField] float duration;
        [SerializeField] float delay;
        [SerializeField] Ease ease;
        [SerializeField] Vector2 direction;
        RectTransform rect;
        Vector2 enabledPosition;
        Vector2 disabledPosition;
        // Start is called before the first frame update
        Vector2 localPosition;

        /*
        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            Vector2 anchorMin=rect.anchorMin;
            Vector2 anchorMax=rect.anchorMax;
            Vector2 pivot=rect.pivot;
            if (direction.x > 0)
            {
                anchorMin.x = 0;
                anchorMax.x = 0;
                pivot.x = 0;
            }
            else if (direction.x < 0)
            {
                anchorMin.x = 1;
                anchorMax.x = 1;
                pivot.x = 1;
            }

            if (direction.y > 0)
            {
                anchorMin.y = 0;
                anchorMax.y = 0;
                pivot.y = 0;
            }
            else if (direction.y < 0)
            {
                anchorMin.y = 1;
                anchorMax.y = 1;
                pivot.y = 1;
            }
            //Vector3 deltaPosition = rectTransform.anchorMin - anchorMin;
            localPosition = rect.localPosition;
            rect.SetPivot(pivot);
            //rect.anchorMin = anchorMin;
            //rect.anchorMax = anchorMax;
            //rect.SetAnchorMax(anchorMax);
            //rect.SetAnchorMin(anchorMin);

            //float canvasScale = GetComponentInParent<CanvasScaler>().referenceResolution.y/ Screen.height;
            //float width = rect.parent.GetComponent<RectTransform>().rect.width * canvasScale;

            //enabledPosition = rect.localPosition;

            //Vector2 difference = Vector2.zero;
            //if (direction.x > 0)
            //{
            //    difference.x = enabledPosition.x;
            //}
            //else if (direction.x < 0)
            //{
            //    difference.x = -width + enabledPosition.x;
            //}

            //disabledPosition.x = enabledPosition.x - difference.x;
            //disabledPosition.y = enabledPosition.y;
            //Debug.Log(disabledPosition);


            enabledPosition = rect.anchoredPosition;
            disabledPosition = new Vector2(enabledPosition.x - (Mathf.Abs(enabledPosition.x) + rect.rect.width) * direction.x, enabledPosition.y - (Mathf.Abs(enabledPosition.y) + rect.rect.height) * direction.y);
        }
        */
        //Vector2 GetMargins(Vector2 direction)
        //{
        //    Vector2 margins;
        //    if(direction.x>1)
        //    {
        //        margins.x=rect.localPosition.x;
        //    }else
        //    {

        //    }
        //    return margins;
        //}
        /*
        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            RectTransform parentRect = rect.parent.GetComponent<RectTransform>();
            float canvasScale = rect.GetComponentInParent<CanvasScaler>().referenceResolution.y / Screen.height;
            Vector2 pivot = rect.pivot;

            if (direction.x > 0)
            {
                pivot.x = 1;
            }
            else if (direction.x < 0)
            {
                pivot.x = 0;
            }

            if (direction.y > 0)
            {
                pivot.y = 00;
            }
            else if (direction.y < 0)
            {   
                pivot.y = 1;
            }

            //rect.SetPivot(pivot);
            enabledPosition = rect.position;


            Vector2 difference = enabledPosition;
            if (direction.x > 0)
            { 
                difference.x = parentRect.rect.xMin;
            }
            else if (direction.x < 0)
            {
                difference.x = parentRect.rect.xMax;
            }

            if (direction.y > 0)
            {
                difference.y = parentRect.rect.yMin;
            }
            else if (direction.y < 0)
            {
                difference.y = parentRect.rect.yMax;
            }

            disabledPosition = difference;
            Debug.Log(difference, parentRect.gameObject);

            //enabledPosition = rect.anchoredPosition;
            //disabledPosition = new Vector2(enabledPosition.x - (Mathf.Abs(enabledPosition.x) + rect.rect.width) * direction.x, enabledPosition.y - (Mathf.Abs(enabledPosition.y) + rect.rect.height) * direction.y);
        }
        */

        private void Awake()
        {
            if(rect==null)
                Setup();
        }

        void Setup()
        {
            if (rect != null) return;

            rect = GetComponent<RectTransform>();

            Vector2 anchorMin = rect.anchorMin;
            Vector2 anchorMax = rect.anchorMax;
            Vector2 pivot = rect.pivot;
            if (direction.x > 0)
            {
                anchorMin.x = 0;
                anchorMax.x = 0;
                pivot.x = 0;
            }
            else if (direction.x < 0)
            {
                anchorMin.x = 1;
                anchorMax.x = 1;
                pivot.x = 1;
            }

            if (direction.y > 0)
            {
                anchorMin.y = 0;
                anchorMax.y = 0;
                pivot.y = 0;
            }
            else if (direction.y < 0)
            {
                anchorMin.y = 1;
                anchorMax.y = 1;
                pivot.y = 1;
            }
            
            localPosition = rect.localPosition;
            rect.SetPivot(pivot);
            
            enabledPosition = rect.anchoredPosition;
            disabledPosition = new Vector2(enabledPosition.x - (Mathf.Abs(enabledPosition.x) + rect.rect.width) * direction.x, enabledPosition.y - (Mathf.Abs(enabledPosition.y) + rect.rect.height) * direction.y);
        }

        public void TweenEnable()
        {
            TweenEnable(duration);
        }
        public void TweenEnable(float duration)
        {
            //rect.DOAnchorPos(enabledPosition, duration).SetEase(ease);
            TweenEnable(duration, delay);
        }

        private void Deactivate()
        {
            rect.anchoredPosition = disabledPosition;
            rect.DOComplete();
        }
        void OnEnable()
        {
            if (autoEnable)
                TweenEnable();
            else
                rect.anchoredPosition = enabledPosition;
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
            rect.DOAnchorPos(disabledPosition, duration).SetEase(ease).SetUpdate(true).onComplete = DisableNow;
        }
        public void DisableNow()
        {
            if(autoEnable)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Deactivate();
        }

        public void TweenEnable(float duration, float delay)
        {
            if (rect == null)
                Setup();

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                if (autoEnable)
                    return;
            }
            if (!gameObject.activeInHierarchy)
                return;

            Deactivate();
            rect.DOAnchorPos(enabledPosition, duration).SetDelay(delay).SetEase(ease).SetUpdate(true);
        }
    }
}