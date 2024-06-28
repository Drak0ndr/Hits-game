using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject _player;

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player" || other.name == "Collider")
        {
            _player.transform.position = new Vector3(-82f, 27.5f, -445f);
        }
    }
}
