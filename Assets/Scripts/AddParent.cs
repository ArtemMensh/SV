using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddParent : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Text _textErrorPass;
    [SerializeField] private Text _textCode;
    [SerializeField] private GameObject _errorPass;
#pragma warning restore 0649

    void Start()
    {
        if (PlayerPrefs.HasKey("jwt"))
        {
            var jwt = PlayerPrefs.GetString("jwt");

            StartCoroutine(Send(jwt));
        }


    }

    private IEnumerator Send(string jwt)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("token", jwt));

        UnityWebRequest www = UnityWebRequest.Get("https://sv.egipti.com/api/children/code?token="+jwt);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            ShowErrorPass(true, "Возникла ошибка при входе");
        }
        else
        {
            string results = www.downloadHandler.text;
            var json = JsonConvert.DeserializeObject<ForJsonAddParent>(results);
            _textCode.text = "Введите данный код на телефоне ребенка \n" + json.response.code;
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
}
