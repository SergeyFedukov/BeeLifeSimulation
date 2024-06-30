using UnityEngine;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI[] _texts;
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

    private void Update()
    {
        _texts[0].text = "flower : " + _map.GetFlowerCount();
        _texts[1].text = "weed : " + _map.GetWeedCount();
        _texts[2].text = "bee worker : " + _map.GetBeesCount();
        _texts[3].text = "bee queen : " + _map.GetBeeQueenCount();
        _texts[4].text = "hive : " + _map.GetHiveCount();

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void StartSimulation()
    {
        _startButton.interactable = false;
        _finishButton.interactable = true;

        foreach (var slider in _sliders)
            slider.interactable = false;

        Flower flower = Instantiate(_flowerPrefub, transform.position, Quaternion.identity);
        flower.TryInitialize(_sliders[0].value, _sliders[1].value, (int)_sliders[2].value);
        Weed weed = Instantiate(_weedPrefub, transform.position, Quaternion.identity);
        weed.TryInitialize(_sliders[3].value, _sliders[4].value, _sliders[5].value);
        Hive hive = Instantiate(_hivePrefub, transform.position, Quaternion.identity);
        BeeWorker beeWorker = new BeeWorker(null, Vector3.zero, _sliders[6].value, _sliders[7].value, (int)_sliders[8].value, _sliders[9].value, (int)_sliders[10].value, _sliders[11].value);
        BeeQueen beeQueen = new BeeQueen(_sliders[12].value, _sliders[13].value, (int)_sliders[14].value, _sliders[15].value);
        hive.TryInitialize(beeQueen, beeWorker, (int)_sliders[16].value);
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