using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    [SerializeField] AudioSource[] _audio;

    public void StartMusic()
    {
        _audio[0].Play(); // BackGround Music
    }
    public void StopMusic()
    {
        _audio[0].Stop();
    }

    public void SlowMoEffect(float modificator)
    {
        foreach (AudioSource sound in _audio)
        {
            sound.pitch = modificator;
        }
    }


}
