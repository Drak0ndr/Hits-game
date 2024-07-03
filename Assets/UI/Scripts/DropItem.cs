using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class DropItem : MonoBehaviour, IPointerClickHandler
    {
        public List<GameObject> Items;

        public Transform ItemContent;

        private List<int> EnvelopesId = new List<int>() { 6, 7, 8, 9, 10, 11 };

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            foreach(Item item in GlobalsVar.Items)
            {
                if(item.icon == this.gameObject.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite)
                {
                    Vector3 objPosition = GameObject.Find("Player").transform.position;
                    objPosition.z -= 1.5f;
                    objPosition.y += 1f;

                    switch (item.id)
                    {
                        case 1:
                            Instantiate(Items[0], objPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(Items[1], objPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(Items[2], objPosition, Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(Items[3], objPosition, Quaternion.identity);
                            break;
                        case 5:
                            Instantiate(Items[4], objPosition, Quaternion.identity);
                            break;
                        case 6:
                            GlobalsVar.currEnvelope = 1;
                            break;
                        case 7:
                            GlobalsVar.currEnvelope = 2;
                            break;
                        case 8:
                            GlobalsVar.currEnvelope = 3;
                            break;
                        case 9:
                            GlobalsVar.currEnvelope = 4;
                            break;
                        case 10:
                            GlobalsVar.currEnvelope = 5;
                            break;
                        case 11:
                            GlobalsVar.currEnvelope = 6;
                            break;
                    }

                    if (!EnvelopesId.Contains(item.id))
                    {
                        Destroy(this.gameObject);

                        GlobalsVar.Items.Remove(item);
                    }

                    break;
                }
            }
        }
    }
}
