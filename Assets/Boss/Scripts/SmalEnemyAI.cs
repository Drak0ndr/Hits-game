using Player;
using UnityEngine;

public class SmalEnemyAI : MonoBehaviour
{
    public GameObject _bullet;
    public Transform _bulletPoint;
    public GameObject _explosion;

    private float timeBetweenAttacks;
    private float shootForce = 20f;

    private bool alreadyAttacked = false;

    private GameObject _player;

    private float HP = 2f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (HP <= 0)
        {
            Vector3 position = this.gameObject.transform.position;

            GameObject newExplosion = Instantiate(_explosion, position, Quaternion.identity);
            Destroy(newExplosion, 0.8f);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FireBall" || other.name == "MagicSpall" || other.name == "Stone(Clone)" ||
            other.name == "FireBall(Clone)" || other.name == "MagicSpall(Clone)")
        {
            HP -= 1f;
        }
    }

    private void LateUpdate()
    {
        if (GlobalsVar.isFight)
        {
            transform.LookAt(_player.transform.position);

            this.transform.position = Vector3.MoveTowards(transform.position,
                    _player.transform.position + new Vector3(0f, ((float)Random.Range(25, 35)) / 10f, 0f), Time.deltaTime * 1.5f);

            if (!alreadyAttacked)
            {
                Vector3 dirWithSpread = _player.transform.position - _bulletPoint.position + new Vector3(0f, 2f, 0f);

                GameObject currentBullet = Instantiate(_bullet, _bulletPoint.position, Quaternion.identity);

                currentBullet.transform.forward = dirWithSpread.normalized;

                currentBullet.GetComponent<Rigidbody>().AddForce(dirWithSpread.normalized * shootForce, ForceMode.Impulse);

                alreadyAttacked = true;

                timeBetweenAttacks = Random.Range(20, 40) / 10f;

                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
