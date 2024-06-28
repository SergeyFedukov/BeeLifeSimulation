using UnityEngine;

public abstract class Bee
{
    private float _lifetime;
    private float _timeUntilDeath;

    public bool IsAlive { get; private set; }

    public Bee(float lifetime)
    {
        _lifetime = lifetime;
        _timeUntilDeath = lifetime;
        IsAlive = true;
    }

    public virtual void ChangeState()
    {
        if (IsAlive)
            _timeUntilDeath -= Time.deltaTime;

        if (_timeUntilDeath <= 0)
            IsAlive = false;
    }
}