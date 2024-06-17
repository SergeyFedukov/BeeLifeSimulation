using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefub;
    [SerializeField] private Flower _flowerPrefub;
    [SerializeField] private Weed _weedPrefub;

    private void Start()
    {
        Map map = Map.GetInstance(Vector3.zero, 1, 4, 4, 8);
        map.TryCreate(_cellPrefub, 0.2f);
        map.PlacePlantInCell(_flowerPrefub);
        map.PlacePlantInCell(_weedPrefub);
    }
}