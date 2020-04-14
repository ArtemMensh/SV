using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangePasswordClick()
    {
        SceneManager.LoadScene("PasswordRec");
        Debug.Log("Congratulations change password");
    }

    public void OnEnterClick()
    {
        Debug.Log("Congratulations sign in");
    }

    public void OnSignUpClick()
    {
        SceneManager.LoadScene("RegistrationScene");
        Debug.Log("Congratulations sign up");
    }
}
