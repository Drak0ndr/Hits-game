using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class DropItem : MonoBehaviour, IPointerClickHandler
    {
        public GameObject pr1;
        public GameObject pr2;
        public GameObject pr3;
        public GameObject pr4;

        public Transform ItemContent;
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            foreach(Item item in Globals.Items)
            {
                if(item.icon == this.gameObject.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite)
                {
                    Vector3 objPosition = GameObject.Find("Player").transform.position;
                    objPosition.z += 1.5f;
                    objPosition.y += 1f;

                    switch (item.id)
                    {
                        case 1:
                            Instantiate(pr1, objPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(pr2, objPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(pr3, objPosition, Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(pr4, objPosition, Quaternion.identity);
                            break;
                    }
                    
                    Destroy(this.gameObject);

                    Globals.Items.Remove(item);

                    break;
                }
            }
        }
    }
}
