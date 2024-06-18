using Player;
using UnityEngine;

public class IncreaseMANA : MonoBehaviour
{
    private void Update()
    {
        if(this.gameObject != null)
        {
            Destroy(this.gameObject, 6f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Player" || other.name == "Cylinder")
            && gameObject != null && GlobalsVar.isSpecialMagicalAbilities)
        {
            GlobalsVar.PlayerMANA += 5f;
            Destroy(this.gameObject);
        }
    }
}
