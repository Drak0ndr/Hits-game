using Player;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> gameObjects;

    private float spawnRadius = 300f;

    private bool alreadyIncreaseHP = false;
    private bool alreadyIncreaseMANA = false;
    private bool alreadyDecreaseHP = false;


    private void LateUpdate()
    {
        if (GlobalsVar.isFight)
        {
            var rand1 = Random.Range(0, 100);
            var rand2 = Random.Range(0, 100);
            var rand3 = Random.Range(0, 100);

            if (rand1 > 80 && !alreadyIncreaseHP)
            {
                alreadyIncreaseHP = true;

                ShowIncreaseHP();
            }

            if (rand2 > 80 && !alreadyIncreaseMANA)
            {
                alreadyIncreaseMANA = true;

                ShowIncreaseMANA();
            }

            if (rand3 > 60 && !alreadyDecreaseHP)
            {
                alreadyDecreaseHP = true;

                ShowDecreaseMANA();
            }
        } 
    }

    private void ShowIncreaseHP()
    {
        if(this.gameObject != null)
        {
            var rand = Random.Range(1, 5);

            for (int i = 0; i < rand; ++i)
            {
                var newPoint = this.transform.position;

                newPoint.x += (Random.Range(-spawnRadius, spawnRadius) / 10f);
                newPoint.z += (Random.Range(-spawnRadius, spawnRadius) / 10f);

                Instantiate(gameObjects[0], newPoint, Quaternion.identity);
            }
        }

        Invoke(nameof(ResetIncreaseHP), 40f);
    }

    private void ShowIncreaseMANA()
    {
        if(this.gameObject != null)
        {
            var rand = Random.Range(1, 5);

            for (int i = 0; i < rand; ++i)
            {
                var newPoint = this.transform.position;

                newPoint.x += (Random.Range(-spawnRadius, spawnRadius) / 10f);
                newPoint.z += (Random.Range(-spawnRadius, spawnRadius) / 10f);

                Instantiate(gameObjects[1], newPoint, Quaternion.identity);
            } 
        }

        Invoke(nameof(ResetIncreaseMANA), 38f);
    }

    private void ShowDecreaseMANA()
    {
        if (this.gameObject != null)
        {
            var rand = Random.Range(1, 5);

            for (int i = 0; i < rand; ++i)
            {
                var newPoint = this.transform.position;

                newPoint.x += (Random.Range(-spawnRadius, spawnRadius) / 10f);
                newPoint.z += (Random.Range(-spawnRadius, spawnRadius) / 10f);

                Instantiate(gameObjects[2], newPoint, Quaternion.identity);
            } 
        }

        Invoke(nameof(ResetDecreaseHP), 30f);
    }

    private void ResetDecreaseHP()
    {
        alreadyDecreaseHP = false;
    }

    private void ResetIncreaseHP()
    {
        alreadyIncreaseHP = false;
    }

    private void ResetIncreaseMANA()
    {
        alreadyIncreaseMANA = false;
    }
}
