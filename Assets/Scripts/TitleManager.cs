using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void toFirstLevel()
    {
        SceneManager.LoadScene("Start(Template)");
    }

    public void quit()
    {
        Application.Quit();
    }
}
