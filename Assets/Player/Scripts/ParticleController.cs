using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private bool collided;

    void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag is "Bullet" && co.gameObject.tag is "Player" && !collided)
        {
            collided = true;
            Destroy(co.gameObject);
        }
    }
}
