using System.Collections.Generic;
using UnityEngine;

public class MagicSpall : MonoBehaviour
{
    public GameObject _explosion;

    private List<string> _colliderNames = new List<string>() { "Body", "Crystals", "Eye" };
    private bool isHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag is not "Player" && collision.gameObject.tag is not "MagicBall" 
            && collision.collider.name is not "Collider" && !isHit)
        {
            isHit = true;

            GameObject newExplosion = Instantiate(_explosion, collision.contacts[0].point, Quaternion.identity);
            Destroy(newExplosion, 0.8f);

            print(collision.collider.name);

            Destroy(gameObject); 
        }
    }
}
