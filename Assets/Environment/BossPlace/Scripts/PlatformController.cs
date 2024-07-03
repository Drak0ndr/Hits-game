using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject _portal;
    public GameObject _player;
    public Transform _portalPosition;
    private void OnTriggerExit(Collider other)
    {
        if ((other.name == "Player" || other.name == "Collider" || other.name == "Cylinder"))
        {
            GameObject newPortal = Instantiate(_portal, _portalPosition.position + new Vector3(0f, 1f, 0f), Quaternion.identity);

            _player.transform.position = _portalPosition.position;

            Destroy(newPortal, 4f);
        }
    }
}
