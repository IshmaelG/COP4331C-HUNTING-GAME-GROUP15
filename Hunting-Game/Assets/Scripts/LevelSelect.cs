using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void OpenMenu()
    {
        if (SaveManager.instance.hasLoaded)
        {
            gameObject.SetActive(true);
        }  
    }
    public void PlayGame(int l)
    {
        if (l == 1) SceneManager.LoadScene("level1");
        if (l == 2) SceneManager.LoadScene("level2");
        if (l == 3) SceneManager.LoadScene("level3");
    }
    public void PlaySample()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
