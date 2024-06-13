using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.EventSystems;
using System;
using Player;


namespace Player
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        public Transform ItemContent;
        public GameObject InventoryItem;

        private void Awake()
        {
            Instance = this;
        }

        public void Add(Item item)
        {
            GlobalsVar.Items.Add(item);
            ListItems();
        }

        public void Remove(Item item)
        {
            GlobalsVar.Items.Remove(item);
            ListItems();
        }

        public void ListItems()
        {
            foreach (Transform item in ItemContent)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in GlobalsVar.Items)
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);

                var itemIcon = obj.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();

                itemIcon.sprite = item.icon;

            }
        }
    }
}
