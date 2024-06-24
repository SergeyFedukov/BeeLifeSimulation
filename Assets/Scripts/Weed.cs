using UnityEngine;

public class Weed : Plant
{
    [SerializeField] private float _damage;

    protected override void Awake()
    {
        base.Awake();
    }

    public bool TryInitialize(float lifetime, float breedingTime, float damage)
    {
        if (!_isInitialized)
            _damage = damage;
        return TryInitialize(lifetime, breedingTime);
    }

    public override void ChangeState(Cell topCell, Cell bottomCell, Cell leftCell, Cell rightCell)
    {
        base.ChangeState(topCell, bottomCell, leftCell, rightCell);
        Fight(topCell, bottomCell, leftCell, rightCell);
    }

    private void Fight(Cell topCell, Cell bottomCell, Cell leftCell, Cell rightCell)
    {
        Cell[] cells = { topCell, bottomCell, leftCell, rightCell };

        foreach (var cell in cells)
        {
            if (cell != null && cell.StateObject != null && cell.StateObject is Flower)
            {
                Flower flower = (Flower)cell.StateObject;
                flower.TakeDamage(_damage * Time.deltaTime);
                break;
            }
        }
    }
}