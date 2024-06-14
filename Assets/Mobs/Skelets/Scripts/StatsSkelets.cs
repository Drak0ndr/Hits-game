using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class StatsSkelets : MonoBehaviour
{
    public GameObject _hand;
    public Animator _animator;
    public GameObject _explosion;

    private float HP = 100f;
    private bool isDeath = false;

    private async void OnCollisionEnter(Collision col)
    {
        if (this.gameObject != null && (col.collider.name == "Player" || col.collider.name == "Collider"))
        {
            float dist = Vector3.Distance(_hand.transform.position, transform.position);

            if (dist < 1.5f && _animator.GetBool("isAttacking") && this.gameObject != null && !isDeath)
            {
                HP -= 7.5f;

                if (this.gameObject != null && !isDeath)
                {
                    Vector3 objPosition = this.transform.position;
                    objPosition.x += 1f;

                    if (this.gameObject != null && !isDeath)
                    {
                        this.gameObject.transform.position = objPosition;
                    }
                }

                if (HP <= 0 && this.gameObject != null)
                {
                    isDeath = true;

                    await Task.Delay(800);

                    if(this.gameObject != null)
                    {
                        Vector3 objPosition = this.transform.position;

                        GameObject newExplotion = Instantiate(_explosion, objPosition, Quaternion.identity);

                        Destroy(this.gameObject);

                        await Task.Delay(800);

                        Destroy(newExplotion);
                    } 
                } 
            }  
        } 
    }
}
