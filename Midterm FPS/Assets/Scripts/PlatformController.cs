using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject _platform;
    public Vector3 _destination;
    public float _duration;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(lerpPosition(_destination, _duration));
    }

    IEnumerator lerpPosition(Vector3 destination, float duration)
    {
        float time = 0;
        Vector3 startPosition = _platform.transform.position;
        while (time < duration)
        {
            _platform.transform.position = Vector3.Lerp(startPosition, destination, time / duration);
            time += Time.deltaTime;

            yield return null;
        }
        _platform.transform.position = destination;

        time = 0;
        while (time < duration)
        {
            _platform.transform.position = Vector3.Lerp(destination, startPosition, time / duration);
            time += Time.deltaTime;

            yield return null;
        }
    }

}
