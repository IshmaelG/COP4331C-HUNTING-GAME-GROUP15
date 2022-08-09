using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatLevel : MonoBehaviour
{
    GameObject[] enemied;
    public static int enemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemied = GameObject.FindGameObjectsWithTag("deer");
        enemies = enemied.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies == 0)
        {
            Debug.Log("Level Completed!");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
