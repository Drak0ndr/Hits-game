using Player;
using UnityEngine;

public class EnvelopePickUp : MonoBehaviour
{
    public Item Item;

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player" || other.name == "Collider")
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
