using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SlightDelay(1.3f);        
    }

    void SlightDelay(float duration)
    {
        StartCoroutine(ShowSplash(duration));
    }

    IEnumerator ShowSplash(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
