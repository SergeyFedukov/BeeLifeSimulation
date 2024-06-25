using UnityEngine;

public class Cell : MonoBehaviour
{
    private Cell _topAdjacentCell;
    private Cell _bottomAdjacentCell;
    private Cell _leftAdjacentCell;
    private Cell _rightAdjacentCell;
    private IStateObject _stateObject;

    public bool CanTakeStateObject { get; private set; }

    public IStateObject StateObject { get { return _stateObject; } }

    private void Awake()
    {
        CanTakeStateObject = true;
    }

    private void Update()
    {
        _stateObject?.ChangeState(_topAdjacentCell, _bottomAdjacentCell, _leftAdjacentCell, _rightAdjacentCell);

        if (_stateObject is Plant)
        {
            Plant plant = (Plant)_stateObject;
            if (plant != null && !plant.IsAlive)
            {
                CanTakeStateObject = true;
                Destroy(plant.gameObject);
                _stateObject = null;
            }
        }
        else 
        {
            
        }
    }

    public bool TryAddAdjacentCells(Cell topAdjacentCell, Cell bottomAdjacentCell, Cell leftAdjacentCell, Cell rightAdjacentCell)
    {
        if (_topAdjacentCell != null || _bottomAdjacentCell != null || _leftAdjacentCell != null || _rightAdjacentCell != null)
            return false;
        _topAdjacentCell = topAdjacentCell;
        _bottomAdjacentCell = bottomAdjacentCell;
        _leftAdjacentCell = leftAdjacentCell;
        _rightAdjacentCell = rightAdjacentCell;
        return true;
    }

    public bool TryTakeStateObject(IStateObject stateObject)
    {
        if (!CanTakeStateObject) return false;
        if (stateObject is Plant)
            _stateObject = Instantiate((Plant)stateObject, transform.position, Quaternion.identity);
        else
            _stateObject = Instantiate((Hive)stateObject, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
        CanTakeStateObject = false;
        return true;
    }
}