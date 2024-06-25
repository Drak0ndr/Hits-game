using UnityEditorInternal;
using UnityEngine;

public class ShowTitles : MonoBehaviour
{
    public GameObject _finalTitles;

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player" || other.name == "Collider")
        {
            _finalTitles.SetActive(true);
        }
    }
}
