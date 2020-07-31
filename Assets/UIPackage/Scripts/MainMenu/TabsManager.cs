using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcAid.UI
{
    public class TabsManager : MonoBehaviour, IUIItemsGroup
    {
        [SerializeField] List<Tab> tabs;

        public List<Tab> Tabs { get => tabs; private set => tabs = value; }

        public List<IHandleUIItems> TabHandlers { get; private set; }

        public List<IUIItemsController> TabControllers { get; private set; }

        public int SelectedItemIndex { get; private set; }

        private void Awake()
        {
            TabHandlers = new List<IHandleUIItems>();
            TabControllers = new List<IUIItemsController>();

            foreach (var item in GetComponentsInChildren<IHandleUIItems>())
            {
                TabHandlers.Add(item);
            }
            foreach (var item in GetComponentsInChildren<IUIItemsController>())
            {
                TabControllers.Add(item);
            }
            Setup();
            if (Tabs.Count > 0)
                ItemSelected(0);
        }

        void Setup()
        {
            for (int i = Tabs.Count - 1; i >= 0; i--)
            {
                if (Tabs[i] == null)
                    Tabs.RemoveAt(i);
            }

            int index = 0;
            foreach (var item in Tabs)
            {
                item.Setup(index);
                index++;
            }
            foreach (var tabController in TabControllers)
            {
                tabController.Setup(this);
            }
            foreach (var tabHandler in TabHandlers)
            {
                tabHandler.Setup(Tabs);
            }
        }

        public void ItemSelected(Tab item)
        {
            if (!Tabs.Contains(item)) return;

            SelectedItemIndex = item.Index;
            foreach (var tab in TabHandlers)
            {
                tab.ItemSelected(item);
            }
        }
        public void ItemSelected(int itemIndex)
        {
            if (itemIndex >= Tabs.Count)
                return;
            if (itemIndex < 0)
                return;

            ItemSelected(Tabs[itemIndex]);
        }

        public bool SelectNextItem()
        {
            if (SelectedItemIndex + 1 >= Tabs.Count)
                return false;

            ItemSelected(Tabs[SelectedItemIndex+1]);

            if (SelectedItemIndex + 1 >= Tabs.Count)
                return false;
            else return true;
        }

        public bool SelectPreviousItem()
        {
            if (SelectedItemIndex - 1 < 0)
                return false;

            ItemSelected(Tabs[SelectedItemIndex-1]);
          
            if (SelectedItemIndex - 1 < 0)
                return false;
            else return true;
        }

    }

}