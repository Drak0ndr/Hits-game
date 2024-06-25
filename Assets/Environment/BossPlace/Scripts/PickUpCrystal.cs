using UnityEngine;

public class PickUpCrystal : MonoBehaviour
{
    public GameObject _portal;

    private GameObject _crystal;

    private float pickUpRange = 1f;

    private void Update()
    {
        _crystal = GameObject.FindGameObjectWithTag("Crystal");

        if (_crystal != null)
        {
            float dist = Vector3.Distance(_crystal.transform.position, transform.position);

            if (dist < pickUpRange)
            {
                PickUp();
            }
        } 
    }

    private void PickUp()
    {
        _portal.SetActive(true);

        Destroy(_crystal);
    }
}
