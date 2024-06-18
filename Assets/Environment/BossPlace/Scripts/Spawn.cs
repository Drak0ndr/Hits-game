using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> gameObjects;

    private float spawnRadius = 30f;

    private bool timeBetweenIncreaseHP = false;
    private bool timeBetweenIncreaseMANA = false;
    private bool timeBetweenDecreaseHP = false;


    private void LateUpdate()
    {
        var rand1 = Random.Range(0, 100);
        var rand2 = Random.Range(0, 100);
        var rand3 = Random.Range(0, 100);

        if (rand1 > 80 && !timeBetweenIncreaseHP)
        {
            timeBetweenIncreaseHP = true;

            ShowIncreaseHP();
        }

        if (rand2 > 80 && !timeBetweenIncreaseMANA)
        {
            timeBetweenIncreaseMANA = true;

            ShowIncreaseMANA();
        }

        if (rand3 > 60 && !timeBetweenDecreaseHP)
        {
            timeBetweenDecreaseHP = true;

            ShowDecreaseMANA();
        }
    }

    private async void ShowIncreaseHP()
    {
        var rand = Random.Range(1, 5);

        for(int i = 0; i < rand; ++i)
        {
            var newPoint = this.transform.position;

            newPoint.x += (Random.Range(-spawnRadius, spawnRadius));
            newPoint.z += (Random.Range(-spawnRadius, spawnRadius));

            Instantiate(gameObjects[0], newPoint, Quaternion.identity);
        }

        await Task.Delay(40000);

        timeBetweenIncreaseHP = false;
    }

    private async void ShowIncreaseMANA()
    {
        var rand = Random.Range(1, 5);

        for (int i = 0; i < rand; ++i)
        {
            var newPoint = this.transform.position;

            newPoint.x += (Random.Range(-spawnRadius, spawnRadius));
            newPoint.z += (Random.Range(-spawnRadius, spawnRadius));

            Instantiate(gameObjects[1], newPoint, Quaternion.identity);
        }

        await Task.Delay(38000);

        timeBetweenIncreaseMANA = false;
    }

    private async void ShowDecreaseMANA()
    {
        var rand = Random.Range(1, 5);

        for (int i = 0; i < rand; ++i)
        {
            var newPoint = this.transform.position;

            newPoint.x += (Random.Range(-spawnRadius, spawnRadius));
            newPoint.z += (Random.Range(-spawnRadius, spawnRadius));

            Instantiate(gameObjects[2], newPoint, Quaternion.identity);
        }

        await Task.Delay(30000);

        timeBetweenDecreaseHP = false;
    }
}
