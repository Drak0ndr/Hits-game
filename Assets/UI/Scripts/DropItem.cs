using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class DropItem : MonoBehaviour, IPointerClickHandler
    {
        public GameObject prefab1;
        public GameObject prefab2;
        public GameObject prefab3;
        public GameObject prefab4;

        public Transform ItemContent;
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            foreach(Item item in GlobalsVar.Items)
            {
                if(item.icon == this.gameObject.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite)
                {
                    Vector3 objPosition = GameObject.Find("Player").transform.position;
                    objPosition.z += 1.5f;
                    objPosition.y += 1f;

                    switch (item.id)
                    {
                        case 1:
                            Instantiate(prefab1, objPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(prefab2, objPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(prefab3, objPosition, Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(prefab4, objPosition, Quaternion.identity);
                            break;
                    }
                    
                    Destroy(this.gameObject);

                    GlobalsVar.Items.Remove(item);

                    break;
                }
            }
        }
    }
}
