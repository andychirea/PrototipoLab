using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _intensity = 0.01f;

    private Vector3 _startPos;

    public void SetIntensity(float intensity) => _intensity = intensity;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        var x = Mathf.Sin(Time.time * _speed) * _intensity;
        var y = Mathf.Cos(Time.time * _speed) * _intensity;
        transform.position = _startPos + new Vector3(x, y, 0f);
    }
}
