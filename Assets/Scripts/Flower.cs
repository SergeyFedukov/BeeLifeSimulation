using UnityEngine;

public class Flower : Plant
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void TakeDamage(float damage)
    {
        _lifetime -= damage;
    }
}
