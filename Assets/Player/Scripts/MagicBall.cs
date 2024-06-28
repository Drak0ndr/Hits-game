using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Player
{
    public class MagicBall : MonoBehaviour
    {
        public GameObject _explosion;
        public GameObject _frog;
        public GameObject _hedgehog;
        public GameObject _newDoor;
        public GameObject _dialog;
        public GameObject _envelope;

        private List<string> _colliderNames = new List<string>() { "Body", "Crystals", "Eye" };
        private float magicBallLife = 3f;
        private bool isFrog = true;
        private bool isHit = false;

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

                    if (isFrog)
                    {
                        StartDialog();
                    }
                }

                else if (collision.gameObject.tag is "Door")
                {
                    var boards = _newDoor.GetComponentsInChildren<Renderer>();

                    foreach (Renderer board in boards)
                    {
                        board.GetComponent<Rigidbody>().isKinematic = false;
                    }

                    Instantiate(_newDoor, new Vector3(-140f, 15.2f, -288f), Quaternion.identity);

                    Destroy(collision.gameObject);
                }

                else if (collision.gameObject.tag is "Pumpkin")
                {
                    Vector3 position = gameObject.transform.position;

                    Destroy(collision.gameObject);

                    Instantiate(_envelope, position, Quaternion.identity);
                }

                else if (_colliderNames.Contains(collision.collider.name) && !isHit)
                {
                    isHit = true;

                    GlobalsVar.EnemyHP -= 3.5f;
                }
            }
        }

        private async void StartDialog()
        {
            await Task.Delay(1500);

            GlobalsVar.isThirdDialog = true;

            isFrog = false;

            Cursor.visible = true;
        }
    }
}

