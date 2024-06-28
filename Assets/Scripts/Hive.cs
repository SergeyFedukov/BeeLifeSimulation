using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour, IStateObject
{ 
    [SerializeField] private BeeWorkerView _prefubBeeWorkerView;
    private int _beeCapacity;
    private List<BeeWorker> _bees = new List<BeeWorker>();
    private BeeQueen _beeQueen = new BeeQueen(30f, 2f);

    private void Awake()
    {
        _beeCapacity = 1;
    }

    public void ChangeState(Cell topCell, Cell bottomCell, Cell leftCell, Cell rightCell)
    {
        _beeQueen?.ChangeState();
        if (_bees.Count < _beeCapacity)
        {
            BeeWorker beeWorker = _beeQueen?.TryBreedBee(_prefubBeeWorkerView, transform.position + new Vector3(0, 0.2f, -0.6f), 60f, 1f, 2, 1f);
            if (beeWorker != null)
                _bees.Add(beeWorker);
        }
        else
        {
            Cell[] cells = { topCell, bottomCell, leftCell, rightCell };

            foreach (var cell in cells)
                if (cell != null && cell.TryTakeStateObject(this))
                    break;
        }

        foreach (var bee in _bees)
            bee.ChangeState();
            
    }
}