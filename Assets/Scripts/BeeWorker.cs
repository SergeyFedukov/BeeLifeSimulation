using UnityEngine;

public class BeeWorker : Bee
{
    private BeeWorkerView _prefubBeeWorkerView;
    private float _restTime;
    private float _timeUntilWork;
    private float _workTime;
    private float _tempWorkTime;
    private BeeWorkerView _view;
    private Vector3 _viewPosition;

    public BeeWorker(BeeWorkerView prefubBeeWorkerView, Vector3 viewPosition, float lifetime, float restTime, float workTime) : base(lifetime)
    {
        _prefubBeeWorkerView = prefubBeeWorkerView;
        _viewPosition = viewPosition;
        _restTime = restTime;
        _workTime = workTime;
        _tempWorkTime = workTime;
        _timeUntilWork = 0;
    }

    public override void ChangeState()
    {
        base.ChangeState();

        if (_timeUntilWork <= 0)
        {
            if (_view == null)
                _view = Object.Instantiate(_prefubBeeWorkerView, _viewPosition, Quaternion.identity);
            if (_tempWorkTime > 0)
                _tempWorkTime -= Time.deltaTime;
            else
            {
                Object.Destroy(_view.gameObject);
                _view = null;
                _tempWorkTime = _workTime;
                _timeUntilWork = _restTime;
            }
        }
        else
            _timeUntilWork -= Time.deltaTime;
    }
}