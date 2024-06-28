using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopesListener : MonoBehaviour
{
    public List<GameObject> Envelopes = new List<GameObject>();

    void Update()
    {
        if(GlobalsVar.currEnvelope != -1)
        {
            switch(GlobalsVar.currEnvelope)
            {
                case 0:
                    break;
                case 1:
                    Envelopes[0].SetActive(true);
                    Cursor.visible = false;
                    break;
                case 2:
                    Envelopes[1].SetActive(true);
                    Cursor.visible = false;
                    break;
                case 3:
                    Envelopes[2].SetActive(true);
                    Cursor.visible = false;
                    break;
                case 4:
                    Envelopes[3].SetActive(true);
                    Cursor.visible = false;
                    break;
                case 5:
                    Envelopes[4].SetActive(true);
                    Cursor.visible = false;
                    break;
                case 6:
                    Envelopes[5].SetActive(true);
                    Cursor.visible = false;
                    break;
            }
        }
    }
}
