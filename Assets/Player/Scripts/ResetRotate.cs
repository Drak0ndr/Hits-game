using Player;
using UnityEngine;

public class ResetRotate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" || other.name == "Collider")
        {
            GlobalsVar._magicRotate = 0f;
        }
    }
}
