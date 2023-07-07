using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("----- Platform Info -----")]
    public GameObject _platform;
    public Vector3 _destination;
    public float _duration;
    public bool needsTargets;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.player.transform.SetParent(_platform.transform);
            if (needsTargets && gameManager.instance.getTargetCount() <= 0)
            {
                StartCoroutine(lerpPosition(_destination, _duration));
            }
            else if (!needsTargets)
                StartCoroutine(lerpPosition(_destination, _duration));
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.player.transform.SetParent(null);
        }
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
