using UnityEngine;

public class BeeQueen : Bee
{
    private float _breedingTime;
    private float _timeUntilBreeding;
    private bool _canBreed;

    public BeeQueen(float lifetime, float breedingTime) : base(lifetime)
    {
        _breedingTime = breedingTime;
        _timeUntilBreeding = breedingTime;
        _canBreed = false;
    }

    public BeeWorker TryBreedBee(BeeWorkerView prefubBeeWorkerView, Vector3 viewPosition, float lifetime, float restTime, float workTime)
    {
        BeeWorker bornBee = null;
        if (_canBreed)
        {
            _timeUntilBreeding = _breedingTime;
            bornBee = new BeeWorker(prefubBeeWorkerView, viewPosition, lifetime, restTime, workTime);
            _canBreed = false;
        }
        return bornBee;
    }

    public override void ChangeState()
    {
        base.ChangeState();

        if (IsAlive)
        {
            if (!_canBreed)
                _timeUntilBreeding -= Time.deltaTime;
            if (_timeUntilBreeding <= 0)
                _canBreed = true;
        }
    }
}