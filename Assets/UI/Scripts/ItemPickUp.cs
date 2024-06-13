using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Player
{
    public class ItemPickUp : MonoBehaviour
    {
        public Item Item;

        private float pickUpRange = 1.3f;

        private void Update()
        {
            float dist = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);

            if (dist < pickUpRange)
            {
                PickUp();
            }
        }

        private void PickUp()
        {
            InventoryManager.Instance.Add(Item);
            Destroy(gameObject);
        }
    }
}
