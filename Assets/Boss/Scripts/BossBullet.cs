using Player;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject _explosion;
    private void OnCollisionEnter(Collision collision)
    {
        if((collision.collider.name == "Player" || collision.collider.name == "Cylinder")
            && gameObject != null)
        {
            GameObject newExplosion = Instantiate(_explosion, this.gameObject.transform.position, Quaternion.identity);
            Destroy(newExplosion, 0.8f);

            GlobalsVar.PlayerHP -= 5f;

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 10f);
        }
    }
}
