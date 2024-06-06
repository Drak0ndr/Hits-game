using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public Transform RHFirePoint;
    public float projectileSpeed = 25;
    public float fireRate = 4;


    private Vector3 destination;
    private float timeToFire;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootProjective();
        }
    }

    private void ShootProjective()
    {
        float middleW = cam.pixelWidth / 2;
        float middleH = cam.pixelHeight / 2;
        Ray ray = cam.ScreenPointToRay(new Vector3(middleW, middleH, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(10000);
        }

        InstantiateProjective(RHFirePoint);
    }

    private void InstantiateProjective(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

    }
}
