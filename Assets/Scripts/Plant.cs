using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    private float _breedingTime;
    private float _timeBeforeBreeding;
    private bool _canBreed;

    protected virtual void Awake()
    {
        _breedingTime = 2f;
        _timeBeforeBreeding = _breedingTime;
        _canBreed = false;
    }

    public void FightForLife(Cell topCell, Cell bottomCell, Cell leftCell, Cell rightCell)
    {
        Cell[] cells = { topCell, bottomCell, leftCell, rightCell };

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