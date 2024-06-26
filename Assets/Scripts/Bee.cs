using UnityEngine;

[System.Serializable]
public abstract class Bee
{
    [SerializeField] private float _lifetime;
    private float _timeUntilDeath;
    [SerializeField] private float _satietyTime;
    private float _tempSatietyTime;
    [SerializeField] private int _amountOfPollenForSatiety;

    public bool IsAlive { get; private set; }
    public float Lifetime { get { return _lifetime; } }
    public float SatietyTime { get { return _satietyTime; } }
    public int AmountOfPollenForSatiety { get { return _amountOfPollenForSatiety; } }

    public Bee(float lifetime, float satietyTime, int amountOfPollenForSatiety)
    {
        _lifetime = lifetime;
        _satietyTime = satietyTime;
        _amountOfPollenForSatiety = amountOfPollenForSatiety;
        _tempSatietyTime = _satietyTime;
        _timeUntilDeath = lifetime;
        IsAlive = true;
    }

    public virtual void ChangeState()
    {
        if (IsAlive)
        {
            _timeUntilDeath -= Time.deltaTime;
            _tempSatietyTime -= Time.deltaTime;

            if (_tempSatietyTime <= 0)
                _timeUntilDeath -= Time.deltaTime;
        }

        if (_timeUntilDeath <= 0)
            IsAlive = false;
    }

    public void TryEat(ref int amountOfPollen)
    {
        if (_tempSatietyTime <= 0  && amountOfPollen >= _amountOfPollenForSatiety)
        {
            amountOfPollen -= _amountOfPollenForSatiety;
            _tempSatietyTime = _satietyTime;
        }
    }
}