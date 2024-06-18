using Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    /*

    //Attacking
    
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    */

    public GameObject _player;
    public GameObject _bullet;
    public Transform _bulletPoint;
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


    private async void LateUpdate()
    {
        float dist = Vector3.Distance(_player.transform.position, transform.position);
        /*print(dist);*/

        if ( dist < maxMovementDist && dist > maxFightDist)
        {
            isHarassment = true;

            float step = harassmentSpeed * Time.deltaTime;

            transform.LookAt(_player.transform.position);

            this.transform.position = Vector3.MoveTowards(transform.position, 
                _player.transform.position + new Vector3(0f, ((float)Random.Range(45, 65)) / 10f, 0f), step);
        }
        else if(dist < maxFightDist)
        {
            isAttack = true;

            float step = harassmentSpeed * Time.deltaTime;

            transform.LookAt(_player.transform.position);

            this.transform.position = Vector3.MoveTowards(transform.position,
                _player.transform.position + new Vector3(0f, ((float)Random.Range(45, 65)) / 10f, 0f), step);

            if (!alreadyAttacked)
            {
                /*Rigidbody rb = Instantiate(_bullet, transform.position_bulletPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 5f, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);*/

                Vector3 dirWithSpread = _player.transform.position - _bulletPoint.position + new Vector3(0f, 1f, 0f);

                GameObject currentBullet = Instantiate(_bullet, _bulletPoint.position, Quaternion.identity);
                 
                currentBullet.transform.forward = dirWithSpread.normalized;

                currentBullet.GetComponent<Rigidbody>().AddForce(dirWithSpread.normalized * shootForce, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        else{
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

                Task.Delay(2000);
            }

            else
            {
                Movement();
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

        if(this.transform.position == points[index].transform.position)
        {
            isMovement = false;
        }
    }
}
