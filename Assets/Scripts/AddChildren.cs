using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddChildren : MonoBehaviour
{

#pragma warning disable 0649

    [SerializeField] private Text _textErrorPass;
    [SerializeField] private InputField _inpudCode;
    [SerializeField] private GameObject _errorPass;

#pragma warning restore 0649

    public void OK()
    {

        StartCoroutine(Send(_inpudCode.text));

    }


    private IEnumerator Send(string code)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("code", code));
        
        UnityWebRequest www = UnityWebRequest.Post("https://sv.egipti.com/api/children", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            ShowErrorPass(true, "Возникла ошибка");
        }
        else
        {
            string results = www.downloadHandler.text;
            var json = JsonConvert.DeserializeObject<ForJsonLogin>(results);
            var jwt = json.response.jwt;
            if(PlayerPrefs.HasKey("ParentToken"))
            {
                var str = PlayerPrefs.GetString("ParentToken");
                str += "," + jwt;
                PlayerPrefs.SetString("ParentToken", str);
            }
            else
            {
                PlayerPrefs.SetString("ParentToken", jwt);
                ShowErrorPass(true, "Sucsessful");
            }
            //SceneManager.LoadScene("MainScence");
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
