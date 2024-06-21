using UnityEngine;

public class Cell : MonoBehaviour
{
    private Cell _topAdjacentCell;
    private Cell _bottomAdjacentCell;
    private Cell _leftAdjacentCell;
    private Cell _rightAdjacentCell;
    private Plant _plant;

    public bool CanTakePlant { get; private set; }

    public Plant Plant { get { return _plant; } }

    private void Awake()
    {
        CanTakePlant = true;
    }

    private void Update()
    {
        _plant?.ChangeLifeState(_topAdjacentCell, _bottomAdjacentCell, _leftAdjacentCell, _rightAdjacentCell);

        if (_plant != null && !_plant.IsAlive)
        {
            CanTakePlant = true;
            Destroy(_plant.gameObject);
            _plant = null;
        }
    }

    public bool TryAddAdjacent—ells(Cell topAdjacentCell, Cell bottomAdjacentCell, Cell leftAdjacentCell, Cell rightAdjacentCell)
    {
        if (_topAdjacentCell != null || _bottomAdjacentCell != null || _leftAdjacentCell != null || _rightAdjacentCell != null)
            return false;
        _topAdjacentCell = topAdjacentCell;
        _bottomAdjacentCell = bottomAdjacentCell;
        _leftAdjacentCell = leftAdjacentCell;
        _rightAdjacentCell = rightAdjacentCell;
        return true;
    }

    public bool TryTakePlant(Plant plant)
    {
        if (!CanTakePlant) return false;
        _plant = Instantiate(plant, transform.position, Quaternion.identity);
        CanTakePlant = false;
        return true;
    }
}