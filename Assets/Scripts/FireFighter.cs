using UnityEngine;

public class FireFighter : UsibleObject
{
    [SerializeField] private ParticleSystem _spray;
    [SerializeField] private float _capacity = 100f;
    [SerializeField] private float _emptyingStep = 1f;

    private float _currentCapacity;

    void Start()
    {
        if (_spray == null)
            _spray = GetComponentInChildren<ParticleSystem>();

        _currentCapacity = _capacity;
    }

    public override void Use()
    {
        if (_currentCapacity <= 0f)
        {
            Usable = false;
        }
        else
            _spray.gameObject.SetActive(true);
            
        _spray.Play();
        _currentCapacity -= _emptyingStep;
    }
}
