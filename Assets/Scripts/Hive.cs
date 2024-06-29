using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour, IStateObject
{ 
    [SerializeField] private BeeWorkerView _prefubBeeWorkerView;
    [SerializeField] private int _beeCapacity;
    private List<BeeWorker> _bees = new List<BeeWorker>();
    private BeeQueen _beeQueen = new BeeQueen(30f, 40f, 4, 2f);
    private int countOfPollen;

    private void Awake()
    {
        _beeCapacity = 1;
        countOfPollen = 0;
    }

    public void ChangeState(Cell topCell, Cell bottomCell, Cell leftCell, Cell rightCell)
    {
        _beeQueen?.ChangeState();

        _bees.RemoveAll(bee => !bee.IsAlive);

        foreach (var bee in _bees)
        {
            int pollen = bee.TryGetPollen();
            if (countOfPollen + pollen <= int.MaxValue)
                countOfPollen += pollen;
        }

        _beeQueen.TryEat(ref countOfPollen);
        foreach (var bee in _bees)
            if (bee.IsInHive)
                bee.TryEat(ref countOfPollen);

        if (_bees.Count < _beeCapacity)
        {
            BeeWorker beeWorker = _beeQueen?.TryBreedBee(_prefubBeeWorkerView, transform.position + new Vector3(0, 0.2f, -0.6f), 60f, 4f, 2, 1f, 2, 1f);
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