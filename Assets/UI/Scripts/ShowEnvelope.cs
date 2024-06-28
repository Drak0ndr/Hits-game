using UnityEngine;

public class ShowEnvelope : MonoBehaviour
{
    public GameObject _envelope;
    void Update()
    {
        if(_envelope != null)
        {
            _envelope.SetActive(true);
        }
    }
}
