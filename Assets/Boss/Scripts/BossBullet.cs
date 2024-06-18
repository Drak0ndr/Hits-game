using Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BossBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if((collision.collider.name == "Player" || collision.collider.name == "Cylinder")
            && gameObject != null)
        {
            GlobalsVar.PlayerHP -= 5f;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 10f);
        }
    }
}
