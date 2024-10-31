using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private PointType _type;

    public PointType GetPointType()
    {
        return _type;
    }
}
