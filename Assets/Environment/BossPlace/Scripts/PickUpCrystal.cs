using UnityEngine;

public class PickUpCrystal : MonoBehaviour
{
    public GameObject _portal;

    public GameObject _crystal;

    private float pickUpRange = 1f;

    private void Update()
    {
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
