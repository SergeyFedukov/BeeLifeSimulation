using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour, IStateObject
{
    [SerializeField] private BeeWorkerView _prefubBeeWorkerView;
    private List<BeeWorker> _bees = new List<BeeWorker>();
    private BeeQueen _beeQueen = new BeeQueen(10f, 2f);

    public void ChangeState(Cell topCell, Cell bottomCell, Cell leftCell, Cell rightCell)
    {
        _beeQueen.ChangeState();
        BeeWorker beeWorker = _beeQueen.TryBreedBee(_prefubBeeWorkerView, transform.position + new Vector3(1f, 1f, -3f), 10f, 3f, 6f);
        if (beeWorker != null)
            _bees.Add(beeWorker);

        foreach (var bee in _bees)
            bee.ChangeState();
            
    }
}