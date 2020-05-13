using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go : MonoBehaviour
{
    public void GoChildrenScence()
    {
        SceneManager.LoadScene("AddChildren");
    }

    public void GoParentScence()
    {
        SceneManager.LoadScene("AddParent");
    }

    public void GoAddScence()
    {
        SceneManager.LoadScene("Add");
    }

    public void GoMainScence()
    {
        SceneManager.LoadScene("MainScence");
    }

    public void GoProfile()
    {
        SceneManager.LoadScene("Profile");
    }
}
