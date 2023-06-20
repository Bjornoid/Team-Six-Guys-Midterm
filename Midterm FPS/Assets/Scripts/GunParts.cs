using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParts : MonoBehaviour
{
    public List<GameObject> gunList;
    public List<GameObject> triggers;
    [SerializeField] GameObject gunPart1;
    GameObject gunPart2;
    GameObject gunPart3;

    
    int fullGun = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.Equals(triggers[0]))
            {
                gunList.Add(gunPart1);
                gunPart1.SetActive(false);
            }
            else if (gameObject.Equals(triggers[1]))
            {
                gunList.Add(gunPart2);
                gunPart2.SetActive(false);
            }
            else if (gameObject.Equals(triggers[2]))
            {
                gunList.Add(gunPart3);
                gunPart3.SetActive(false);
            }
        }
    }
}
