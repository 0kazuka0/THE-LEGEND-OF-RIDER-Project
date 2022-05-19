using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menu : MonoBehaviour
{
    public void statclick()
    {
        SceneManager.LoadScene(1);
    }
    public void exitclick()
    {
        Application.Quit();
    }
}
