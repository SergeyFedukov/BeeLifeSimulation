using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    protected float _lifetime;
    private float _breedingTime;
    private float _timeBeforeBreeding;
    private bool _canBreed;

    public bool IsAlive { get; private set; }

    protected virtual void Awake()
    {
        _lifetime = 5f;
        _breedingTime = 2f;
        _timeBeforeBreeding = _breedingTime;
        _canBreed = false;
        IsAlive = true;
    }

    public virtual void ChangeLifeState(Cell topCell, Cell bottomCell, Cell leftCell, Cell rightCell)
    {
        Cell[] cells = { topCell, bottomCell, leftCell, rightCell };

        if (IsAlive)
        {
            TryBreed(cells);
            _lifetime -= Time.deltaTime;
        }

        if (_lifetime <= 0)
            IsAlive = false;
    }

    private void TryBreed(Cell[] cells)
    {
        _timeBeforeBreeding -= Time.deltaTime;
        if (_timeBeforeBreeding <= 0)
            _canBreed = true;

        if (_canBreed)
        {
            foreach (var cell in cells)
            {
                if (cell != null && cell.TryTakePlant(this))
                {
                    _canBreed = false;
                    _timeBeforeBreeding = _breedingTime;
                    break;
                }
            }
        }
    }
}