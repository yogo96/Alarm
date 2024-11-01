using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _signalVolumeMin = 0f;
    private float _signalVolumeMax = 1f;
    private float _signalVolumeStep = 0.5f;
    private float _targetSignalVolume;
    private Coroutine _switchSignalCoroutine;
    private bool _isStopSignal;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _signalVolumeMin;
    }

    private void OnDisable()
    {
        StopSwitchVolumeCoroutine();
    }

    public void PlaySignal()
    {
        _isStopSignal = false;
        _targetSignalVolume = _signalVolumeMax;
        RestartSwitchVolumeCoroutine();
    }

    public void StopSignal()
    {
        _isStopSignal = true;
        _targetSignalVolume = _signalVolumeMin;
        RestartSwitchVolumeCoroutine();
    }

    private void RestartSwitchVolumeCoroutine()
    {
        StopSwitchVolumeCoroutine();
        _switchSignalCoroutine = StartCoroutine(VolumeSignalSwitching());
    }
    
    private void StopSwitchVolumeCoroutine()
    {
        if (_switchSignalCoroutine != null) 
            StopCoroutine(_switchSignalCoroutine);
    }

    private IEnumerator VolumeSignalSwitching()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();
        
        while (_audioSource.volume != _targetSignalVolume)
        {
            _audioSource.volume = Mathf
                .MoveTowards(_audioSource.volume, _targetSignalVolume, _signalVolumeStep * Time.deltaTime);
            yield return null;
        }
        
        if (_isStopSignal)
            _audioSource.Stop();
    }
}