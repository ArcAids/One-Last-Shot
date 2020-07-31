using System.Collections.Generic;
using UnityEngine;

namespace ArcAid.UI
{
    public interface IUIItemsGroup
    {
        List<Tab> Tabs { get; }
        List<IHandleUIItems> TabHandlers { get; }
        List<IUIItemsController> TabControllers{ get; }

        int SelectedItemIndex { get; } 
        bool SelectNextItem();
        bool SelectPreviousItem();
        void ItemSelected(Tab item);
        void ItemSelected(int itemIndex);
 
    }

    public interface IHandleUIItems
    {
        void Setup(in List<Tab> items);
        void ItemSelected(Tab item);
    }

    public interface IUIItemsController
    {
        void Setup(IUIItemsGroup group);
        void SelectItem(int index);
        void SelectNextItem();
        void SelectPreviousItem();
    }
    public interface IUIGroupItem
    {
        string Name { get; }
        int Index { get; }
        void Setup(int index);

        void OnSelect();
        void OnDeselect();
    }

    public interface ITweenSwitch
    {
        void TweenEnable();
        void TweenEnable(float duration);
        void TweenEnable(float duration, float delay);
        void TweenDisable();
        void TweenDisable(float duration);
    }
}