using UnityEngine;

public class CarEffectController : MonoBehaviour
{
    [SerializeField] private float _valueToStartParticles;

    [SerializeField] private ParticleSystem _particleSystemLeft;
    [SerializeField] private ParticleSystem _particleSystemRight;
    [SerializeField] private ParticleSystem _particleSystemNitro;

    [SerializeField] private TrailRenderer _trailRendererLeft;
    [SerializeField] private TrailRenderer _trailRendererRight;

    private float _particleRate;

    private void Start()
    {

    }


    public void UpdateEffect(float move)
    {
        if(Mathf.Abs(move) == _valueToStartParticles)
        {
            _particleSystemLeft.Play();
            _particleSystemRight.Play();


        } else
        {
            _particleSystemLeft.Stop();
            _particleSystemRight.Stop();
        }
    }
    public void SetNitro()
    {
        _particleSystemNitro.Play();
    }
    public void ResetNitro()
    {
        _particleSystemNitro.Stop();
    }
}
