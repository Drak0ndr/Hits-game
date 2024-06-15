using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssistantMovement : MonoBehaviour
{
    public GameObject _player;

    private float maxDistance = 3.5f;
    private float movementSpeed = 2f;

    void FixedUpdate()
    {
        if (_player != null)
        {
            float dist = Vector3.Distance(_player.transform.position, transform.position);

            if (dist > maxDistance)
            {
                float step = movementSpeed * Time.deltaTime;

                transform.LookAt(_player.transform.position);

                this.transform.position = Vector3.MoveTowards(transform.position, _player.transform.position + 
                    new Vector3(getRandom(0f, 1.5f), getRandom(1f, 2f), getRandom(-2f, -1.5f)), step);
            }
        }
    }

    private float getRandom(float min, float max)
    {
        System.Random rnd = new System.Random();

        return (float)rnd.NextDouble() * (max - min) + min;
    }
}
