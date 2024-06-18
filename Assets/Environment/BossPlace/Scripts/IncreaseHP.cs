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
            GlobalsVar.PlayerHP += 5f;
            Destroy(this.gameObject);
        }
    }
}
