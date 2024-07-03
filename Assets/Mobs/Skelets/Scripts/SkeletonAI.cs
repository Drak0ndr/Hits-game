using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public GameObject _circle;
    public GameObject _explosion;

    private float HP = 1f;

    private void Update()
    {
        if(HP <= 0)
        {
            Vector3 position = this.gameObject.transform.position;

            GameObject newExplosion = Instantiate(_explosion, position, Quaternion.identity);
            Destroy(newExplosion, 0.8f);

            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "MagicBall" || other.name == "MagicBall(Clone)" || other.name == "Stone(Clone)")
        {
            HP -= 1f;
        }
    }

    public void RemoveCircle()
    {
        Destroy(_circle);
    }
}
