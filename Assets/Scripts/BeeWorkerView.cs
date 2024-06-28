using System.Collections.Generic;
using UnityEngine;

public class BeeWorkerView : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _maxFlyPositionX;
    [SerializeField] private int _minFlyPositionX;
    [SerializeField] private int _maxFlyPositionZ;
    [SerializeField] private int _minFlyPositionZ;
    [SerializeField] private float _timeUntilMove;
    private float _tempTimeUntilMove;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private bool _canSeeFlower;
    private bool _canFlewToFlower;
    private Flower _targetFlower;
    private List<Flower> _viewedFlowers;

    public bool IsInHive { get; private set; }

    public int PollenCount { get; private set; }

    public int PollenCapacity { get; set; }

    public float Visibility { get; set; }


    private void Awake()
    {
        _targetPosition = transform.position;
        _startPosition = transform.position;
        _canSeeFlower = false;
        _canFlewToFlower = false;
        _viewedFlowers = new List<Flower>();
        _targetFlower = null;
        _tempTimeUntilMove = _timeUntilMove;
        PollenCount = 0;
        IsInHive = false;
    }

    private void Update()
    {
        Move();

        if (PollenCount >= PollenCapacity)
        {
            _targetPosition = _startPosition;
            _canSeeFlower = false;
        }

        if (_canFlewToFlower)
            _tempTimeUntilMove -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(PollenCount < PollenCapacity)
            FindFlowerPosition();
    }

    private void ChangeTargetPosition()
    {
        System.Random randomX = new System.Random();
        System.Random randomZ = new System.Random();

        float positionX = randomX.Next(_minFlyPositionX, _maxFlyPositionX + 1);
        float positionZ = randomZ.Next(_minFlyPositionZ, _maxFlyPositionZ + 1);

        _targetPosition = new Vector3(positionX, transform.position.y, positionZ);
    }

    private void Move()
    {
        if (transform.position == _targetPosition)
        {
            if (_canSeeFlower)
                _canFlewToFlower = true;
            else if (PollenCount >= PollenCapacity)
                IsInHive = true;
            else
                ChangeTargetPosition();
        }

        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
    }

    private void FindFlowerPosition()
    {
        if (!_canSeeFlower)
        {
            RaycastHit[] hits = new RaycastHit[4];
            Physics.Raycast(transform.position, Vector3.forward, out hits[0], Visibility);
            Physics.Raycast(transform.position, Vector3.back, out hits[1], Visibility);
            Physics.Raycast(transform.position, Vector3.left, out hits[2], Visibility);
            Physics.Raycast(transform.position, Vector3.right, out hits[3], Visibility);

            foreach (var hit in hits)
            {
                
                if (hit.transform != null && hit.transform.gameObject.TryGetComponent<Flower>(out _targetFlower) && !_viewedFlowers.Contains(_targetFlower))
                {
                    _canSeeFlower = true;
                    _targetPosition = new Vector3(_targetFlower.transform.position.x, transform.position.y, _targetFlower.transform.position.z);
                    _viewedFlowers.RemoveAll(viewedFlower => !viewedFlower.IsAlive);
                    break;
                }
            }
        }

        if (_canFlewToFlower)
        {
            if (_targetFlower != null && _targetFlower.IsAlive)
            {
                PollenCount += _targetFlower.GetPollen(PollenCapacity);
                _viewedFlowers.Add(_targetFlower);
                _targetFlower = null;
            }

            if (_tempTimeUntilMove <= 0)
            {
                _canFlewToFlower = false;
                _canSeeFlower = false;
                _tempTimeUntilMove = _timeUntilMove;
            }
        }
    }
}