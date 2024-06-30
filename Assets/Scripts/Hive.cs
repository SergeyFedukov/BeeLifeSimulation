using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour, IStateObject
{ 
    [SerializeField] private BeeWorkerView _prefubBeeWorkerView;
    [SerializeField] private int _beeCapacity;
    [SerializeField] private BeeQueen _beeQueenParameters;
    [SerializeField] private BeeWorker _beeWorkerParameters;
    private BeeQueen _beeQueen;
    private List<BeeWorker> _bees = new List<BeeWorker>();
    private int countOfPollen;
    protected bool _isInitialized = false;

    public int GetBeesCount => _bees.Count;

    public int GetBeeQueenCount => _beeQueen.IsAlive ? 1 : 0;

    private void Awake()
    {
        if (_beeCapacity > 0)
            _beeQueen = new BeeQueen(_beeQueenParameters.Lifetime, _beeQueenParameters.SatietyTime, _beeQueenParameters.AmountOfPollenForSatiety, _beeQueenParameters.BreedingTime);
    }

    public bool TryInitialize(BeeQueen beeQueen, BeeWorker beeWorker, int beeCapacity)
    {
        if (!_isInitialized)
        {
            _beeQueenParameters = beeQueen;
            _beeWorkerParameters = beeWorker;
            _beeCapacity = beeCapacity;
            _isInitialized = true;
        }

        return !_isInitialized;
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

        _beeQueen?.TryEat(ref countOfPollen);
        foreach (var bee in _bees)
            if (bee.IsInHive)
                bee.TryEat(ref countOfPollen);

        if (_bees.Count + 1 < _beeCapacity)
        {
            BeeWorker beeWorker = _beeQueen?.TryBreedBee(_prefubBeeWorkerView, transform.position + new Vector3(0, 0.2f, -0.6f), _beeWorkerParameters.Lifetime, _beeWorkerParameters.SatietyTime, _beeWorkerParameters.AmountOfPollenForSatiety, _beeWorkerParameters.RestTime, _beeWorkerParameters.PollenCapacity, _beeWorkerParameters.Visibility);
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

    public void DestroyBee()
    {
        foreach (var bee in _bees)
            bee.DestroyView();
    }
}