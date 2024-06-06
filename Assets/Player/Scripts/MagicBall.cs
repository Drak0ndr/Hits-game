using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float magicBallLife = 3;

    private void Awake()
    {
        Destroy(gameObject, magicBallLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag is not "Player" && collision.gameObject.tag is not "MagicBall")
        {
            /*Destroy(collision.gameObject);*/
            Destroy(gameObject);
        }      
    }
}
