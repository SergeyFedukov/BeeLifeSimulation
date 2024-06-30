using UnityEngine;

[System.Serializable]
public class BeeQueen : Bee
{
    [SerializeField] private float _breedingTime;
    private float _timeUntilBreeding;
    private bool _canBreed;

    public float BreedingTime { get { return _breedingTime; } }

    public BeeQueen(float lifetime, float satietyTime, int amountOfPollenForSatiety, float breedingTime) : base(lifetime, satietyTime, amountOfPollenForSatiety)
    {
        _breedingTime = breedingTime;
        _timeUntilBreeding = breedingTime;
        _canBreed = false;
    }

    public BeeWorker TryBreedBee(BeeWorkerView prefubBeeWorkerView, Vector3 viewPosition, float lifetime, float satietyTime, int amountOfPollenForSatiety, float restTime, int pollenCapacity, float visibility)
    {
        BeeWorker bornBee = null;
        if (_canBreed)
        {
            _timeUntilBreeding = _breedingTime;
            bornBee = new BeeWorker(prefubBeeWorkerView, viewPosition, lifetime, satietyTime, amountOfPollenForSatiety, restTime, pollenCapacity, visibility);
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