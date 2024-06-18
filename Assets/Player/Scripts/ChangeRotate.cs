using Player;
using UnityEngine;

public class ChangeRotate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" || other.name == "Collider")
        {
            GlobalsVar._magicRotate = 180f;
        }  
    }
}
