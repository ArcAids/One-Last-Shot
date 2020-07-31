using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArcAid.UI
{
    public class TabController : MonoBehaviour, IUIItemsController
    {
        [SerializeField]
        protected List<Button> buttons;
        [SerializeField]
        Button nextButton;
        [SerializeField]
        Button previousButton;
        IUIItemsGroup group;
        public virtual void Setup(IUIItemsGroup group)
        {
            if (group == null)
                return;
            this.group = group;

            if (buttons.Count>0)
            {
                int index = 0;
                foreach (var tab in group.Tabs)
                {
                    Button button = buttons[index];
                    if (button != nextButton && button != previousButton)
                        button.onClick.AddListener(delegate { group.ItemSelected(tab); });
                    index++;
                }
            }

            if (nextButton != null)
                nextButton.onClick.AddListener(delegate { SelectNextItem(); });
            if (previousButton != null)
                previousButton.onClick.AddListener(delegate { SelectPreviousItem(); });
        }

        public virtual void SelectItem(int index)
        {
            if (group == null) return;

            group.ItemSelected(index);
            if (index == 0)
            {
                previousButton.interactable = false;
                nextButton.interactable = true;
            }
            else if(index == group.Tabs.Count-1)
            {
                previousButton.interactable = true;
                nextButton.interactable = false;
            }
        }

        public void SelectNextItem()
        {
            if (group == null) return;
            if (!group.SelectNextItem())
            {
                nextButton.interactable = false;
            }
            previousButton.interactable = true;
        }

        public void SelectPreviousItem()
        {
            if (group == null) return;
            if (!group.SelectPreviousItem())
            {
                previousButton.interactable = false;
            }
                nextButton.interactable = true;
        }
    }

}