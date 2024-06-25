using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip _audioClip;
    public AudioSource _audioSource;
    public GameObject _soundManager;

    public void StartSound()
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
}
