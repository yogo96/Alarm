using UnityEngine;

public class MotionSensor : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            _alarm.PlaySignal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            _alarm.StopSignal();
        }
    }
}