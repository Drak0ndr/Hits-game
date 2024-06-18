using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject _platform;
    void OnTriggerStay(Collider other)
    {
        if ((other.name == "Player" || other.name == "Collider"))
        {
            _platform.transform.position += new Vector3(0f, 2f, 0f) * Time.deltaTime;
        }
    }
}
