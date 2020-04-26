using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegistrationScene : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private InputField _textLogin;
    [SerializeField] private InputField _textPhone;
    [SerializeField] private InputField _textPass;
    [SerializeField] private InputField _textRetryPass;
    [SerializeField] private Text _textErrorPass;

    [SerializeField] private GameObject _errorPass;
#pragma warning restore 0649

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("EnterScene");
    }

    public void OnClickRegistration()
    {
        if(!FillField())
        {
            ShowErrorPass(true, "Не все поля заполнены");
            return;
        }
        if(_textPass.text.Equals(_textRetryPass.text))
        {
            // все верно отправляем запрос на регестрацию
            StartCoroutine(Send(_textLogin.text, _textPass.text, _textPhone.text));
        }
        else
        {
            // выводим сообщение об ошибке
            ShowErrorPass(true, "Пароли не совпадают");
        }
    }

    private bool FillField()
    {
        if (_textLogin.text.Equals("")) return false;
        if (_textPass.text.Equals("")) return false;
        if (_textPhone.text.Equals("")) return false;
        if (_textRetryPass.text.Equals("")) return false;
        return true;
    }

    private void ShowErrorPass(bool IsShow, string massage)
    {
        _errorPass.SetActive(IsShow);
        _textErrorPass.text = massage;
    }

    public void CloseErrorPass()
    {
        ShowErrorPass(false, "");
    }

    private IEnumerator Send(string login, string pass, string phone)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username",  login));
        formData.Add(new MultipartFormDataSection("password",  pass));
        formData.Add(new MultipartFormDataSection("phone", phone));

        UnityWebRequest www = UnityWebRequest.Post("https://sv.egipti.com/api/auth/register", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            ShowErrorPass(true, "Возникла ошибка при регестрации");
        }
        else
        {

            SceneManager.LoadScene("MainScence");
        }
    }
}
