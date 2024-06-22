using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    [SerializeField]protected float _lifetime;
    [SerializeField] private float _breedingTime;
    private float _timeBeforeBreeding;
    private float _timeUntilDeath;
    private bool _canBreed;
    protected bool _isInitialized = false;

    public bool IsAlive { get; private set; }

    protected virtual void Awake()
    {
        _timeBeforeBreeding = _breedingTime;
        _timeUntilDeath = _lifetime;
        _canBreed = false;
        IsAlive = true;
    }

    public bool TryInitialize(float lifetime, float breedingTime)
    {
        if (!_isInitialized)
        {
            _lifetime = lifetime;
            _breedingTime = breedingTime;
            _isInitialized = true;
        }

        return !_isInitialized;
    }

    public virtual void ChangeLifeState(Cell topCell, Cell bottomCell, Cell leftCell, Cell rightCell)
    {
        Cell[] cells = { topCell, bottomCell, leftCell, rightCell };

        if (IsAlive)
        {
            TryBreed(cells);
            _timeUntilDeath -= Time.deltaTime;
        }

        if (_timeUntilDeath <= 0)
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