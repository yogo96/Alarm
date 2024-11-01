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

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.volume = _signalVolumeMin;
        _audioSource.Play();
    }

    private void OnDisable()
    {
        if (_switchSignalCoroutine != null) 
            StopCoroutine(_switchSignalCoroutine);
    }

    public void PlaySignal()
    {
        _targetSignalVolume = _signalVolumeMax;
        RestartSwitchVolumeCoroutine();
    }

    public void StopSignal()
    {
        _targetSignalVolume = _signalVolumeMin;
        RestartSwitchVolumeCoroutine();
    }

    private void RestartSwitchVolumeCoroutine()
    {
        _switchSignalCoroutine = StartCoroutine(VolumeSignalSwitching());
    }

    private IEnumerator VolumeSignalSwitching()
    {
        while (_audioSource.volume != _targetSignalVolume)
        {
            _audioSource.volume = Mathf
                .MoveTowards(_audioSource.volume, _targetSignalVolume, _signalVolumeStep * Time.deltaTime);
            yield return null;
        }
    }
}