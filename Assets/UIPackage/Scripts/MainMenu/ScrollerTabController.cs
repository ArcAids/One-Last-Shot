using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace ArcAid.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollerTabController : MonoBehaviour, IEndDragHandler, IUIItemsController
    {
        [SerializeField][Range(0,1)] float stepThresholdPercentage=0.5f;
        ScrollRect scrollRect;
        float currentItemValue;
        float step;
        float stepThreshold;
        IUIItemsGroup group;

        public void Setup(IUIItemsGroup group)
        {
            this.group = group;
            TryGetComponent(out scrollRect);
            int count=group.Tabs.Count;
            if(count==0)
            {
                count = scrollRect.content.GetComponentsInChildren<Tab>().Length;
            }

            step = 1 / (float)(count - 1);
            stepThreshold =step* stepThresholdPercentage;
            currentItemValue =0;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            float difference = scrollRect.horizontalNormalizedPosition - currentItemValue;
            if (Mathf.Abs(difference) > stepThreshold)
            {
                int direction = difference < 0 ? -1 : 1;
                float stepCount;
                difference = Mathf.Abs(difference);
                difference += (step - stepThreshold);
                stepCount = Mathf.Abs((difference / step) - (difference % step));
                currentItemValue += step * stepCount * direction;
                Debug.Log(currentItemValue);
            }
           SelectItem((int)(currentItemValue / step));
        }
        public virtual void SelectItem(int index)
        {
            group?.ItemSelected(index);
        }

        public void SelectNextItem()
        {
            group?.SelectNextItem();
        }

        public void SelectPreviousItem()
        {
            group?.SelectPreviousItem();
        }
    }
}
