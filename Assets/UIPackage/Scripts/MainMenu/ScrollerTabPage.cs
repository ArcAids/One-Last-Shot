using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ArcAid.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollerTabPage : MonoBehaviour, IHandleUIItems
    {
        [SerializeField] float duration=1;
        [SerializeField] Ease ease;
        ScrollRect scrollRect;
        Tab selectedItem;
        float step;
        float currentRectValue;
        public void Setup(in List<Tab> items)
        {
            TryGetComponent(out scrollRect);
            int count = items.Count;
            if (count == 0)
            {
                count = scrollRect.content.GetComponentsInChildren<Tab>().Length;
            }
            step = 1 / (float)(count - 1);
        }
        public void ItemSelected(Tab item)
        {
            if (selectedItem != null)
                selectedItem.OnDeselect();
            selectedItem = item;
            item.OnSelect();

            currentRectValue = item.Index * step;

            scrollRect.DOHorizontalNormalizedPos(currentRectValue,duration).SetEase(ease);

        }

    }

}