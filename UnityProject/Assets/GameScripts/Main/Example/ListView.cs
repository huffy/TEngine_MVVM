﻿using Framework.Observables;
using System.Collections.Specialized;
using TEngine;
using UnityEngine;

namespace Framework.Tutorials
{
    public class ListView : UIWindow
    {
        private ObservableList<ListItemViewModel> items;

        public Transform content;

        public GameObject itemTemplate;

        public ObservableList<ListItemViewModel> Items
        {
            get { return this.items; }
            set
            {
                if (this.items == value)
                    return;

                if (this.items != null)
                    this.items.CollectionChanged -= OnCollectionChanged;
          
                this.items = value;
                // this.OnItemsChanged();

                if (this.items != null)
                    this.items.CollectionChanged += OnCollectionChanged;
            }
        }

        public override void OnDestroy()
        {
            if (this.items != null)
                this.items.CollectionChanged -= OnCollectionChanged;
        }

        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            // switch (eventArgs.Action)
            // {
            //     case NotifyCollectionChangedAction.Add:
            //         this.AddItem(eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
            //         break;
            //     case NotifyCollectionChangedAction.Remove:
            //         this.RemoveItem(eventArgs.OldStartingIndex, eventArgs.OldItems[0]);
            //         break;
            //     case NotifyCollectionChangedAction.Replace:
            //         this.ReplaceItem(eventArgs.OldStartingIndex, eventArgs.OldItems[0], eventArgs.NewItems[0]);
            //         break;
            //     case NotifyCollectionChangedAction.Reset:
            //         this.ResetItem();
            //         break;
            //     case NotifyCollectionChangedAction.Move:
            //         this.MoveItem(eventArgs.OldStartingIndex, eventArgs.NewStartingIndex, eventArgs.NewItems[0]);
            //         break;
            // }
        }

        // protected virtual void OnItemsChanged()
        // {
        //     int count = this.content.childCount;
        //     for(int i = count - 1; i >= 0; i--)
        //     {
        //         Transform child = this.content.GetChild(i);
        //         GameObject.Destroy(child.gameObject);
        //     }
        //
        //     for (int i = 0; i < this.items.Count; i++)
        //     {
        //         this.AddItem(i, items[i]);
        //     }
        // }
        //
        // protected virtual void AddItem(int index, object item)
        // {
        //     var itemViewGo = CreateWidgetByPrefab<UIWidget>(itemTemplate);
        //     itemViewGo.transform.SetParent(this.content, false);
        //     itemViewGo.transform.SetSiblingIndex(index);
        //     itemViewGo.SetActive(true);
        //
        //     UIView itemView = itemViewGo.GetComponent<UIView>();
        //     itemView.SetDataContext(item);
        // }
        //
        // protected virtual void RemoveItem(int index, object item)
        // {
        //     Transform transform = this.content.GetChild(index);
        //     UIView itemView = transform.GetComponent<UIView>();
        //     if (itemView.GetDataContext() == item)
        //     {
        //         itemView.gameObject.SetActive(false);
        //         Destroy(itemView.gameObject);
        //     }
        // }
        //
        // protected virtual void ReplaceItem(int index, object oldItem, object item)
        // {
        //     Transform transform = this.content.GetChild(index);
        //     UIView itemView = transform.GetComponent<UIView>();
        //     if (itemView.GetDataContext() == oldItem)
        //     {
        //         itemView.SetDataContext(item);
        //     }
        // }
        //
        // protected virtual void MoveItem(int oldIndex, int index, object item)
        // {
        //     Transform transform = this.content.GetChild(oldIndex);
        //     UIView itemView = transform.GetComponent<UIView>();
        //     itemView.transform.SetSiblingIndex(index);
        // }
        //
        // protected virtual void ResetItem()
        // {
        //     for (int i = this.content.childCount - 1; i >= 0; i--)
        //     {
        //         Transform transform = this.content.GetChild(i);
        //         Destroy(transform.gameObject);
        //     }
        // }
    }

}