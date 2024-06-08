using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public GameObject _explosion;

    public float magicBallLife = 3f;

    private void Awake()
    {
        Destroy(gameObject, magicBallLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag is not "Player" && collision.gameObject.tag is not "MagicBall")
        {
            var newExplosion = Instantiate(_explosion, collision.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(newExplosion, 2f);
            /*Destroy(collision.gameObject);*/
            Destroy(gameObject);
        }      
    }
}
