using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Void : MonoBehaviour
{
    [SerializeField] GameObject voidHole;
    [SerializeField] public float attractionStrength;

    List<GameObject> nearbyEnemies = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        VoidSuck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameManager.instance.UpdateGameGoal(-1); // enemy dies

            nearbyEnemies.Remove(other.gameObject);

            Destroy(other.gameObject);
        }
    }

    void VoidSuck()
    {
        foreach (GameObject enemy in nearbyEnemies)
        {
            if (enemy != null)
            {
                Vector3 voidDirection = voidHole.transform.position - enemy.transform.position;
                Vector3 destination = enemy.transform.position + voidDirection.normalized * attractionStrength * Time.deltaTime;

                enemy.transform.position = destination;
            }
        }
    }
}
