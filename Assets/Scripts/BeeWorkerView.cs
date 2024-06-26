using UnityEngine;

public class BeeWorkerView : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _maxFlyPositionX;
    [SerializeField] private int _minFlyPositionX;
    [SerializeField] private int _maxFlyPositionZ;
    [SerializeField] private int _minFlyPositionZ;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        Move();
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
            ChangeTargetPosition();

        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
    }   
}