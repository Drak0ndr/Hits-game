using Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloseEnvelope : MonoBehaviour
{
    public List<GameObject> _envelopes = new List<GameObject>();
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !GlobalsVar.isTitles 
            && _envelopes.Any(envelope => envelope.activeInHierarchy == true))
        {
            foreach (GameObject envelope in _envelopes)
            {
                envelope.SetActive(false);

                Cursor.visible = true;

                GlobalsVar.currEnvelope = -1;
            }
        }
    }

}
