using UnityEngine;

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
            if (Item.id == 5)
            {
                GlobalsVar.Items.Clear();
            }

            InventoryManager.Instance.Add(Item);
            Destroy(gameObject);
        }
    }
}
