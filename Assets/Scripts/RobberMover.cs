using System.Collections;
using UnityEngine;

public class RobberMover : MonoBehaviour
{
    [SerializeField] private Point[] _movePoints;
    [SerializeField] private Robber _robber;

    private int _pointIndex = 0;
    private WaitForSeconds _waitTime;
    private bool _isWaitOnPoint = false;
    private Coroutine _coroutine;
    private float _distanceToPoint = 0.1f;

    private void Start()
    {
        _waitTime = new WaitForSeconds(2);
    }

    private void Update()
    {
        if (_isWaitOnPoint)
            return;

        Vector3 currentPointPosition = _movePoints[_pointIndex].transform.position;
        _robber.MoveTo(currentPointPosition);

        if (Vector3.Distance(_robber.transform.position, currentPointPosition) <= _distanceToPoint)
        {
            if (_movePoints[_pointIndex].GetPointType() == PointType.House)
                RestartCoroutine();

            ChangePointIndex();
        }
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void ChangePointIndex()
    {
        _pointIndex++;

        if (_pointIndex == _movePoints.Length)
        {
            _pointIndex = 0;
        }
    }

    private void RestartCoroutine()
    {
        _coroutine = StartCoroutine(ChangePointWaiting());
    }

    private IEnumerator ChangePointWaiting()
    {
        _isWaitOnPoint = true;
        yield return _waitTime;
        _isWaitOnPoint = false;
    }
}