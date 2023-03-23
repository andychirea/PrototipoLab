using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartbeatSensor : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> _onValueChanged;

    public float maxIntensity = 0.01f;

    public float interval1 = 0.1f;
    public float interval2 = 1f;

    private void OnEnable()
    {
        StartCoroutine(HeartbeatCoroutine());
    }

    private IEnumerator HeartbeatCoroutine()
    {
        while (enabled)
        {
            _onValueChanged.Invoke(maxIntensity);
            yield return new WaitForSeconds(interval1);
            _onValueChanged.Invoke(0);
            yield return new WaitForSeconds(interval1);
            _onValueChanged.Invoke(maxIntensity);
            yield return new WaitForSeconds(interval1);
            _onValueChanged.Invoke(0);


            yield return new WaitForSeconds(interval2);
        }
    }
}
