using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    [SerializeField] private Text _textPass;
    [SerializeField] private Text _textLogin;

    private string _serverPath = "";
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
        var pass = _textPass.text;
        var login = _textLogin.text;

        //Отправляем запрос для входа



        Debug.Log("Congratulations sign in");
    }

    public void OnSignUpClick()
    {
        SceneManager.LoadScene("RegistrationScene");
        Debug.Log("Congratulations sign up");
    }
}
