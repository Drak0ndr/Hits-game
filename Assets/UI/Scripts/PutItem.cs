using Player;
using UnityEngine;

public class PutItem : MonoBehaviour
{
    public Item Item;

    public void Put()
    {
        InventoryManager.Instance.Add(Item);
    }
}
