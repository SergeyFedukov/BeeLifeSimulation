using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefub;
    [SerializeField] private Flower _flowerPrefub;
    [SerializeField] private Weed _weedPrefub;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _finishButton;
    [SerializeField] private Slider[] _sliders;
    [SerializeField] private Hive _hivePrefub;
    private Map _map;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartSimulation);
        _finishButton.onClick.AddListener(FinishSimulation);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveAllListeners();
        _finishButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _finishButton.interactable = false;
        _map = Map.GetInstance(Vector3.left, 1, 4, 4, 8);
        _map.TryCreate(_cellPrefub, 0.2f);
    }

    private void StartSimulation()
    {
        _startButton.interactable = false;
        _finishButton.interactable = true;

        foreach (var slider in _sliders)
            slider.interactable = false;

        Flower flower = Instantiate(_flowerPrefub, transform.position, Quaternion.identity);
        flower.TryInitialize(_sliders[0].value, _sliders[1].value);
        Weed weed = Instantiate(_weedPrefub, transform.position, Quaternion.identity);
        weed.TryInitialize(_sliders[2].value, _sliders[3].value, _sliders[4].value);
        Hive hive = Instantiate(_hivePrefub, transform.position, Quaternion.identity);
        _map.PlacePlantInCell(flower);
        _map.PlacePlantInCell(weed);
        _map.PlaceHiveInCell(hive);
        Destroy(flower.gameObject);
        Destroy(weed.gameObject);
        Destroy(hive.gameObject);
    }

    private void FinishSimulation()
    {
        _map.Destroy();
        _map = Map.GetInstance(Vector3.left, 1, 4, 4, 8);
        _map.TryCreate(_cellPrefub, 0.2f);

        _startButton.interactable = true;
        _finishButton.interactable = false;

        foreach (var slider in _sliders)
            slider.interactable = true;
    }
}