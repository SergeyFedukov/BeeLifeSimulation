using UnityEngine;

public class Flower : Plant
{
    [SerializeField] private int _pollenCount;
    private int _tempPollenCount;

    protected override void Awake()
    {
        base.Awake();
        _tempPollenCount = _pollenCount;
    }

    public bool TryInitialize(float lifetime, float breedingTime, int pollenCount)
    {
        if (!_isInitialized)
            _pollenCount = pollenCount;
        return TryInitialize(lifetime, breedingTime);
    }

    public void TakeDamage(float damage)
    {
        _timeUntilDeath -= damage;
    }

    public int GetPollen(int beeCapacity)
    {
        int pollenCount = 0;
        if (beeCapacity <= _pollenCount)
        {
            _tempPollenCount -= beeCapacity;
            pollenCount = beeCapacity;
        }
        else
        {
            pollenCount = _pollenCount;
            _tempPollenCount = 0;
        }

        return pollenCount;
    }
}