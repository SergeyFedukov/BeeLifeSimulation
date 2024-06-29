using UnityEngine;

public abstract class Bee
{
    private float _lifetime;
    private float _timeUntilDeath;
    private float _satietyTime;
    private float _tempSatietyTime;
    private int _amountOfPollenForSatiety;

    public bool IsAlive { get; private set; }

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
            Debug.Log($"1 amountOfPollen {amountOfPollen} _tempSatietyTime {_tempSatietyTime}  _satietyTime{_satietyTime}");
            amountOfPollen -= _amountOfPollenForSatiety;
            _tempSatietyTime = _satietyTime;
            Debug.Log($"2 amountOfPollen {amountOfPollen} _tempSatietyTime {_tempSatietyTime}  _satietyTime{_satietyTime}");
        }
    }
}