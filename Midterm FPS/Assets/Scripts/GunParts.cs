using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParts : MonoBehaviour
{
    public List<GameObject> triggers;
    [SerializeField] GameObject gunPart1;
    [SerializeField]GameObject gunPart2;
    [SerializeField]GameObject gunPart3;
    [SerializeField] GameObject gunBuilt;
    [SerializeField] GameObject purpleLight;


    int fullGun = 3;

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
                gameManager.instance.partList.Add(gunPart1);
                gunPart1.SetActive(false);
            }
            else if (gameObject.Equals(triggers[1]))
            {
                gameManager.instance.partList.Add(gunPart2);
                gunPart2.SetActive(false);
            }
            else if (gameObject.Equals(triggers[2]))
            {
                gameManager.instance.partList.Add(gunPart3);
                gunPart3.SetActive(false);
            }
            
            if (gameManager.instance.partList.Count == fullGun)
            {
                gunBuilt.SetActive(true);
                purpleLight.SetActive(true);
            }
        }
    }
}
