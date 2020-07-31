using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArcAid.UI
{
    public class Tab : MonoBehaviour, IUIGroupItem
    {
        [SerializeField] string tabTitle;
        [SerializeField] UnityEvent onDeselect;
        [SerializeField] UnityEvent onSelect;
        public string Name => tabTitle;

        public int Index { get; private set; }

        public virtual void OnDeselect()
        {
            onDeselect.Invoke();
        }

        public virtual void OnSelect()
        {
            onSelect.Invoke();
        }

        public void Setup(int index)
        {
            Index = index;
        }
    }

}