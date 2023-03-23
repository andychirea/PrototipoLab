using UnityEngine;
using UnityEngine.Events;

public class MicrophoneSensor : MonoBehaviour
{
    [SerializeField] private float _micLoudness;
    [SerializeField] private float _sensivity = 100.0f;
    [SerializeField] private float _smoothTime = 1.0f;

    [SerializeField] private UnityEvent<float> _onSendSoundSignal;

    private string _currentDevice;
    private AudioClip _clipRecord;
    private bool _isInitialized = false;

    private readonly int _audioSamples = 64;

    private float _currentLoundnessVelocity;

    private void Start() => Initialize();

    void Update()
    {
        _micLoudness = Mathf.SmoothDamp(_micLoudness, GetAverage() * _sensivity, ref _currentLoundnessVelocity, _smoothTime);

        _onSendSoundSignal.Invoke(_micLoudness);
    }

    void OnEnable() => Initialize();

    void OnDisable() => StopMicrophone();

    void OnDestroy() => StopMicrophone();

    private void Initialize()
    {
        if (_isInitialized)
            return;

        if (Microphone.devices.Length == 0)
        {
            Debug.LogWarning("No se detectó micrófonooo!!!");
            return;
        }

        _currentDevice = Microphone.devices[0];
        _clipRecord = Microphone.Start(_currentDevice, true, 999, 44100);

        _isInitialized = true;
    }

    void StopMicrophone()
    {
        if (!_isInitialized)
            return;

        Microphone.End(_currentDevice);

        _isInitialized = false;
    }

    float GetAverage()
    {
        float[] waveData = new float[_audioSamples];
        int micPosition = Microphone.GetPosition(null) - (_audioSamples + 1); // null means the first microphone

        if (micPosition < 0) 
            return 0;

        _clipRecord.GetData(waveData, micPosition);

        float sum = 0.0f;
        for (int i = 0; i < _audioSamples; i++)
        {
            sum += waveData[i] * waveData[i];
        }

        return sum/_audioSamples;
    }
}