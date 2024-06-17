using UnityEngine;

public class Cell : MonoBehaviour
{
    private Cell _topAdjacentCell;
    private Cell _bottomAdjacentCell;
    private Cell _leftAdjacentCell;
    private Cell _rightAdjacentCell;
    private Plant _plant;

    public bool CanTakePlant { get; private set; }

    private void Awake()
    {
        CanTakePlant = true;
    }

    private void Update()
    {
        
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
        _plant = plant;
        Object.Instantiate(plant, transform.position, Quaternion.identity);
        return true;
    }
}