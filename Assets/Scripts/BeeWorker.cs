using UnityEngine;

public class BeeWorker : Bee
{
    private BeeWorkerView _prefubBeeWorkerView;
    private float _restTime;
    private float _timeUntilWork;
    private int _pollenCapacity;
    private float _visibility;
    private BeeWorkerView _view;
    private Vector3 _viewPosition;
    private int _pollenCount;

    public bool IsInHive { get; private set; }

    public BeeWorker(BeeWorkerView prefubBeeWorkerView, Vector3 viewPosition, float lifetime, float satietyTime, int amountOfPollenForSatiety, float restTime, int pollenCapacity, float visibility) : base(lifetime, satietyTime, amountOfPollenForSatiety)
    {
        _prefubBeeWorkerView = prefubBeeWorkerView;
        _viewPosition = viewPosition;
        _restTime = restTime;
        _timeUntilWork = 0;
        _pollenCapacity = pollenCapacity;
        _visibility = visibility;
        _pollenCount = 0;
    }

    public override void ChangeState()
    {
        base.ChangeState();

        if (IsAlive)
        {
            if (_timeUntilWork <= 0)
            {
                if (_view == null)
                {
                    _view = Object.Instantiate(_prefubBeeWorkerView, _viewPosition, Quaternion.identity);
                    _view.PollenCapacity = _pollenCapacity;
                    _view.Visibility = _visibility;
                    IsInHive = false;
                }
                else if (_view.PollenCount >= _pollenCapacity && _view.IsInHive)
                {
                    _pollenCount = _view.PollenCount;
                    Object.Destroy(_view.gameObject);
                    _view = null;
                    _timeUntilWork = _restTime;
                    IsInHive = true;
                }
            }
            else
                _timeUntilWork -= Time.deltaTime;
        }
        else if (_view != null)
            Object.Destroy(_view.gameObject);
    }

    public int TryGetPollen()
    {
        int pollen = 0;
        if (IsAlive && _view == null && _pollenCount != 0)
        {
            pollen = _pollenCount;
            _pollenCount = 0;
        }

        return pollen;
    }

    public void DestroyView()
    { 
        if (_view != null)
            Object.Destroy(_view.gameObject);
    }
}