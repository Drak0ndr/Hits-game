using UnityEngine;

public class StatsSkelets : MonoBehaviour
{
    public GameObject _explosion;
    public GameObject _player;

    private float HP = 100f;
    private bool isDeath = false;
    private bool alreadyAttacked = false;

    private void Update()
    {
        float dist = Vector3.Distance(_player.transform.position, transform.position);

        if(this.gameObject != null && dist <= 0.8f)
        {
            if (Input.GetMouseButtonDown(1) && !isDeath  && !alreadyAttacked)
            {
                HP -= 10f;

                if (this.gameObject != null && !isDeath)
                {
                    Vector3 objPosition = this.transform.position;
                    objPosition.x += 1f;

                    if (this.gameObject != null && !isDeath)
                    {
                        this.gameObject.transform.position = objPosition;
                    }
                }

                if (HP <= 0 && this.gameObject != null)
                {
                    isDeath = true;

                    Invoke(nameof(DestroySkelet), 0.8f);
                }
            }

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), 1.5f);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroySkelet()
    {
        Vector3 objPosition = this.transform.position;

        GameObject newExplotion = Instantiate(_explosion, objPosition, Quaternion.identity);

        Destroy(this.gameObject);

        Destroy(newExplotion, 0.8f);
    }
}
