using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class EnterScene : MonoBehaviour
{

#pragma warning disable 0649

    [SerializeField] private InputField _Pass;
    [SerializeField] private InputField _Login;
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

    public void OnChangePasswordClick()
    {
        SceneManager.LoadScene("PasswordRec");
        Debug.Log("Congratulations change password");
    }

    public void OnEnterClick()
    {
        var pass = _Pass.text;
        var login = _Login.text;

        if (!FillField())
        {
            ShowErrorPass(true, "Не все поля заполнены");
            return;
        }

        //Отправляем запрос для входа
        StartCoroutine(Send(login, pass));


        Debug.Log("Congratulations sign in");
    }

    public void OnSignUpClick()
    {
        SceneManager.LoadScene("RegistrationScene");
        Debug.Log("Congratulations sign up");
    }

    private IEnumerator Send(string login, string pass)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username", login));
        formData.Add(new MultipartFormDataSection("password", pass));

        UnityWebRequest www = UnityWebRequest.Post("https://sv.egipti.com/api/auth/login", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            ShowErrorPass(true, "Возникла ошибка при входе");
        }
        else
        {
           string results = www.downloadHandler.text;
            var g = JsonConvert.DeserializeObject<ForJson>(results); ;
            SceneManager.LoadScene("MainScence");
        }
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

    private bool FillField()
    {
        if (_Login.text.Equals("")) return false;
        if (_Pass.text.Equals("")) return false;
        return true;
    }
}
