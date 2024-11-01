using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _signalVolumeMin = 0f;
    private float _signalVolumeMax = 1f;
    private float _targetSignalVolume;
    private float _signalVolumeStep = 0.5f;
    private bool _isSwitchSignal;
    private Coroutine _switchSignalCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.volume = _signalVolumeMin;
        _audioSource.Play();
    }

    private void Update()
    {
        if (_isSwitchSignal) 
            RestartSwitchVolumeCoroutine();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            _targetSignalVolume = _signalVolumeMax;
            _isSwitchSignal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            _targetSignalVolume = _signalVolumeMin;
            _isSwitchSignal = true;
        }
    }

    private void OnDisable()
    {
        if (_switchSignalCoroutine != null) 
            StopCoroutine(_switchSignalCoroutine);
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

        _isSwitchSignal = false;
    }
}