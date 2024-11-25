using UnityEngine;

public class CarAudioScript : MonoBehaviour
{
    [SerializeField] AudioSource[] _sounds;


    public void PlayEngine()
    {
        _sounds[0].Play();
    }
    public void StopEngine()
    {
        _sounds[0].Stop();
    }
}
