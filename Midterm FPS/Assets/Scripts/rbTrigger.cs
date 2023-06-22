using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rbTrigger : MonoBehaviour
{
    [SerializeField] Rigidbody[] rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < rb.Length; i++)
            {
                rb[i].constraints = RigidbodyConstraints.None;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
