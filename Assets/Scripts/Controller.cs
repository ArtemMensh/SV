
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class Controller : MonoBehaviour
{
    void Awake()
    {
        var objs = GameObject.FindObjectsOfType<Controller>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("ParentToken"))
        {
            StartCoroutine(StartSendPoint(20));
        }

        StartCoroutine(StartRequest(20));
    }

    // Проверяем всех детей
    private IEnumerator StartRequest(float seconds)
    {
        while (true)
        {
            var jwt = PlayerPrefs.GetString("jwt");

            Request(jwt);

            yield return new WaitForSecondsRealtime(seconds);
        }
    }

    private IEnumerator Request(string jwt)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://sv.egipti.com/api/children?token=" + jwt);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
        }
        else
        {
        }
    }


    // данные будут посылаться раз в n секунд
    private IEnumerator StartSendPoint(float seconds)
    {
        while (true)
        {
            var arrayJwt = PlayerPrefs.GetString("ParentToken").Split(',');

            // определяем позицию ребенка 
            var lat = 0;
            var lng = 0;

            foreach (var jwt in arrayJwt)
            {
                SendPointChildren(jwt, lat, lng);
            }

            yield return new WaitForSecondsRealtime(seconds);
        }
    }

    private IEnumerator SendPointChildren(string jwt, int lat, int lng)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("token", jwt));
        formData.Add(new MultipartFormDataSection("lat", lat.ToString()));
        formData.Add(new MultipartFormDataSection("lng", lng.ToString()));

        UnityWebRequest www = UnityWebRequest.Post("https://sv.egipti.com/api/tracker/check", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
        }
        else
        {
        }
    }


}
