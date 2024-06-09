using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public GameObject _explosion;
    public GameObject _frog;
    public GameObject _hedgehog;

    public float magicBallLife = 3f;

    private void Awake()
    {
        Destroy(gameObject, magicBallLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag is not "Player" && collision.gameObject.tag is not "MagicBall" &&
            collision.gameObject.tag is not "Staff")
        {
            var newExplosion = Instantiate(_explosion, collision.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(newExplosion, 0.8f);
            Destroy(gameObject);

            if (collision.gameObject.tag is "Hedgehog")
            {
                if (collision.contacts[0].otherCollider.name == "Spikes")
                {
                    Instantiate(_frog, collision.contacts[0].point, Quaternion.identity);
                    Destroy(collision.gameObject);
                }           
            }

            else if (collision.gameObject.tag is "Frog")
            {
                Instantiate(_hedgehog, collision.contacts[0].point, Quaternion.identity);
                Destroy(collision.gameObject);
                
            }
        }
    }
}
