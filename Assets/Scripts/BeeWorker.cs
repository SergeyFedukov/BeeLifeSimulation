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

    public BeeWorker(BeeWorkerView prefubBeeWorkerView, Vector3 viewPosition, float lifetime, float restTime, int pollenCapacity, float visibility) : base(lifetime)
    {
        _prefubBeeWorkerView = prefubBeeWorkerView;
        _viewPosition = viewPosition;
        _restTime = restTime;
        _timeUntilWork = 0;
        _pollenCapacity = pollenCapacity;
        _visibility = visibility;
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
                }
                else if (_view.PollenCount >= _pollenCapacity && _view.IsInHive)
                {
                    Object.Destroy(_view.gameObject);
                    _view = null;
                    _timeUntilWork = _restTime;
                }
            }
            else
                _timeUntilWork -= Time.deltaTime;
        }
        else if (_view != null)
            Object.Destroy(_view.gameObject);
    }
}