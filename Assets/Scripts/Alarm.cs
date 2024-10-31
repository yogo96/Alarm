using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _signalVolumeMin = 0f;
    private float _signalVolumeMax = 1f;
    private float _targetSignalVolume;
    private float _signalVolumeStep = 0.5f;
    private bool _isActive;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.volume = _signalVolumeMin;
        _audioSource.Play();
    }

    private void Update()
    {
        _audioSource.volume = Mathf
            .MoveTowards(_audioSource.volume, _targetSignalVolume, _signalVolumeStep * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            _targetSignalVolume = _signalVolumeMax;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            _targetSignalVolume = _signalVolumeMin;
        }
    }
}
