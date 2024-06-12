using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MagicBall : MonoBehaviour
{
    public GameObject _explosion;
    public GameObject _frog;
    public GameObject _hedgehog;
    public GameObject _newDoor;

    public float magicBallLife = 3f;

    private void Start()
    {
        var boards = _newDoor.GetComponentsInChildren<Renderer>();
        foreach (Renderer board in boards)
        {
            board.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
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

            else if (collision.gameObject.tag is "Door")
            {
                var boards = _newDoor.GetComponentsInChildren<Renderer>();
                foreach (Renderer board in boards)
                {
                    board.GetComponent<Rigidbody>().isKinematic = false;
                }

                Instantiate(_newDoor, new Vector3(-0.15f, 0f, -14f), Quaternion.identity);

                Destroy(collision.gameObject);
            }
        }
    }
}
