using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI escText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Credits());
        StartCoroutine(DestroyEsc());
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }


    IEnumerator Credits()
    {
        yield return new WaitForSeconds(48);

        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator DestroyEsc()
    {
        yield return new WaitForSeconds(2);

        escText.enabled = false;
    }
}
