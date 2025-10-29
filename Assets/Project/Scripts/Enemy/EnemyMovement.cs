using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _waitTime = 1.5f;
    [SerializeField] private bool _loop = false;

    private Animator _animator;
    private int _currentIndex = 0;
    private bool _isWaiting = false;
    private int _direction = 1;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (_points.Length > 0)
            transform.position = _points[0].position;
    }

    private void Update()
    {
        if (_points.Length < 2 || _isWaiting) return;

        MoveBetweenPoints();
    }

    private void MoveBetweenPoints()
    {
        Transform target = _points[_currentIndex];

        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0f;

       
        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, _rotationSpeed * Time.deltaTime);
        }

        
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        
        if (_animator != null)
            _animator.SetFloat(SpeedHash, _speed);

       
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    private IEnumerator WaitAtPoint()
    {
        _isWaiting = true;

        // Idle
        if (_animator != null)
            _animator.SetFloat(SpeedHash, 0f);

        yield return new WaitForSeconds(_waitTime);

        // Imposta il prossimo punto
        if (_loop)
        {
            _currentIndex = (_currentIndex + 1) % _points.Length;
        }
        else
        {
            if (_currentIndex == _points.Length - 1)
                _direction = -1;
            else if (_currentIndex == 0)
                _direction = 1;

            _currentIndex += _direction;
        }
        _isWaiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LifeController.Instance.LoseLife();
        }
    }
}


