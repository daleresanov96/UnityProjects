using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("EagleFirstTerrain");
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}
