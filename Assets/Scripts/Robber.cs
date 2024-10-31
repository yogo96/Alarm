using UnityEngine;

public class Robber : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4;

    public void MoveTo(Vector3 pointPosition)
    {
        transform.position = Vector3
            .MoveTowards(transform.position, pointPosition, _moveSpeed * Time.deltaTime);

    }
}
