using Player;
using UnityEngine;

public class BossBulletSmall : MonoBehaviour
{
    public GameObject _explosion;

    private bool isDeath = false;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.name == "Player" || collision.collider.name == "Collider")
            && gameObject != null && !isDeath)
        {
            isDeath = true;

            GameObject newExplosion = Instantiate(_explosion, this.gameObject.transform.position, Quaternion.identity);
            Destroy(newExplosion, 0.8f);

            GlobalsVar.PlayerHP -= 1f;

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5f);
        }
    }
}
