using NUnit.Framework.Internal;
using UnityEngine;

public class CarEffectController : MonoBehaviour
{
    [SerializeField] private float _valueToStartParticles;

    [SerializeField] private ParticleSystem _particleSystemLeft;
    [SerializeField] private ParticleSystem _particleSystemRight;
    [SerializeField] private ParticleSystem _particleSystemNitro;

    [SerializeField] private ParticleSystem _particleSystemBoom;

    [SerializeField] private TrailRenderer _trailRendererLeft;
    [SerializeField] private TrailRenderer _trailRendererRight;

    private float _particleRate;

    private bool _checkWheel=true;

    private void Start()
    {
        CheckIonWheels();
    }

    private void CheckIonWheels()
    {
        CarDataScript _carData = DataManager.Instance._currentCarData;
        if (_carData._wheelsId == 2 && (_carData._nameOfCar == "Delorian" || _carData._nameOfCar == "Quadra"))
        {
            _checkWheel = false;
        }
    }


    public void UpdateEffect(float move)
    {
        if(Mathf.Abs(move) == _valueToStartParticles && _checkWheel)
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

    public void Explosion()
    {
        _particleSystemBoom.Play();
    }

    
}
