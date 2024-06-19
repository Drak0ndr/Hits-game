using Player;
using UnityEngine;

public class IncreaseHP : MonoBehaviour
{
    private void Update()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject, 6f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Player" || other.name == "Cylinder")
            && gameObject != null)
        {
            if (GlobalsVar.isFirstFight)
            {
                GlobalsVar.PlayerHP += 2f;
            }
            else
            {
                GlobalsVar.PlayerHP += 5f;
            }
            
            Destroy(this.gameObject);
        }
    }
}
