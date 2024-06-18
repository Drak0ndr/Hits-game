using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject _player;
    public Animator _animator;

    private bool isExit = false;

    private void Update()
    {
        float dist = Vector3.Distance(_player.transform.position, transform.position);
      
        if (!isExit && dist < 2f)
        {
            _animator.SetBool("isOpen", true);

            isExit = true;                    
        }          
    }
}
