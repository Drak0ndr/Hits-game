using UnityEngine;

public class CreateSkelets : MonoBehaviour
{
    public GameObject _skelet;
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player" || other.name == "Collider")
        {
            /*Instantiate(_skelet, new Vector3(12.39f, 14.4f, -314.25f), Quaternion.identity);*/
            /*Instantiate(_skelet, new Vector3(14.9988f, 14.5f, -305.218f), Quaternion.identity);*/
            /*Instantiate(_skelet, new Vector3(22.80719f, 14.5f, -305.440f), Quaternion.identity);*/
            /*Instantiate(_skelet, new Vector3(24.44452f, 14.25f, -310.335f), Quaternion.identity);*/
            /*Instantiate(_skelet, new Vector3(18.33f, 14.75f, -316.511f), Quaternion.identity);*/
        }
    }
}
