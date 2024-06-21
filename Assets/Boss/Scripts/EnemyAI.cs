using Player;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject _player;
    public GameObject _bullet;
    public Transform _bulletPoint;
    public GameObject _explosion;
    public GameObject _crystal;
    public List<Transform> points = new List<Transform>();

    private float movementSpeed = 1.5f;
    private float harassmentSpeed = 1f;
    private float maxMovementDist = 50f;
    private float maxFightDist = 30f;
    private float timeBetweenAttacks = 2.5f;
    private float shootForce = 20f;

    private int index = 0;

    private bool isMovement = true;
    private bool isHarassment = false;
    private bool isAttack = false;
    private bool alreadyAttacked = false;

    private void Update()
    {
        if(GlobalsVar.EnemyHP <= 0)
        {
            Vector3 position = this.gameObject.transform.position;

            GameObject newExplosion = Instantiate(_explosion, position, Quaternion.identity);
            Destroy(newExplosion, 1.8f);

            Instantiate(_crystal, position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
    private void LateUpdate()
    {
        if(GlobalsVar.isFight)
        {
            float dist = Vector3.Distance(_player.transform.position, transform.position);
            /*print(dist);*/

            if (dist < maxMovementDist && dist > maxFightDist)
            {
                isHarassment = true;

                float step = harassmentSpeed * Time.deltaTime;

                transform.LookAt(_player.transform.position);

                this.transform.position = Vector3.MoveTowards(transform.position,
                    _player.transform.position + new Vector3(0f, ((float)Random.Range(45, 65)) / 10f, 0f), step);
            }
            else if (dist < maxFightDist)
            {
                isAttack = true;

                float step = harassmentSpeed * Time.deltaTime;

                transform.LookAt(_player.transform.position);

                this.transform.position = Vector3.MoveTowards(transform.position,
                    _player.transform.position + new Vector3(0f, ((float)Random.Range(45, 65)) / 10f, 0f), step);

                if (!alreadyAttacked)
                {
                    Vector3 dirWithSpread = _player.transform.position - _bulletPoint.position + new Vector3(0f, 1f, 0f);

                    GameObject currentBullet = Instantiate(_bullet, _bulletPoint.position, Quaternion.identity);

                    currentBullet.transform.forward = dirWithSpread.normalized;

                    currentBullet.GetComponent<Rigidbody>().AddForce(dirWithSpread.normalized * shootForce, ForceMode.Impulse);

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
            }
            else
            {
                isHarassment = false;
                isAttack = false;
            }

            if (!isHarassment && !isAttack)
            {
                if (!isMovement)
                {
                    index = Random.Range(0, 4);
                    transform.LookAt(points[index].transform.position);
                    isMovement = true;
                }
                else
                {
                    Movement();
                }
            }
        }   
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Movement()
    {
        float step = movementSpeed * Time.deltaTime;

        transform.LookAt(points[index].transform.position);

        this.transform.position = Vector3.MoveTowards(transform.position, points[index].transform.position, step);

        if (this.transform.position == points[index].transform.position)
        {
            isMovement = false;
        }
    }
}
