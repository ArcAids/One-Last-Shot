using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArcAid.UI
{
    public class TabHeaderGroup : MonoBehaviour, IHandleUIItems
    {
        [SerializeField] protected List<Tab> tabs;
        Tab selectedTab;
        public virtual void Setup(in List<Tab> items)
        {
            if (tabs.Count>0)
            {
                if (tabs.Count != items.Count)
                    Debug.LogError("Mismatch items count!");

                int i = 0;
                foreach (var tab in tabs)
                {
                    tab.Setup(i);
                    i++;
                }
                return;
            }
            else
            {
                foreach (var item in items)
                {

                }
            }
        }

        public virtual void ItemSelected(Tab item)
        {
            if (selectedTab != null)
                selectedTab.OnDeselect();
            if (tabs.Count > 0)
                selectedTab = tabs[item.Index];
            else
                selectedTab= item;

            selectedTab.OnSelect();
        }
        
    }

}