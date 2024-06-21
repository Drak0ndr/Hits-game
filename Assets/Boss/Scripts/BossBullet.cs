using Player;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject _explosion;

    private bool isDeath = false;

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.collider.name == "Player" || collision.collider.name == "Collider")
            && gameObject != null && !isDeath)
        {
            isDeath = true;

            GameObject newExplosion = Instantiate(_explosion, this.gameObject.transform.position, Quaternion.identity);
            Destroy(newExplosion, 0.8f);

            if(GlobalsVar.isFirstFight)
            {
                GlobalsVar.PlayerHP -= 10f;
            }
            else
            {
                GlobalsVar.PlayerHP -= 5f;
            }
            
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 10f);
        }
    }
}
