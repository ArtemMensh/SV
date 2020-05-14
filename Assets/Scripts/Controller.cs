
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        StartCoroutine(StartSendPoint(1));
        StartCoroutine(StartRequest(1));
    }

    // дети будут посылаться раз в n секунд
    private IEnumerator StartRequest(float seconds)
    {
        while (true)
        {
            var jwt = PlayerPrefs.GetString("jwt");

            StartCoroutine(Request(jwt));

            yield return new WaitForSecondsRealtime(seconds);
        }
    }

    // принимаем все данные по детям
    private IEnumerator Request(string jwt)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://sv.egipti.com/api/children?token=" + jwt);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //JObject o = new JObject();
            //o = JObject.Parse();
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
            if (PlayerPrefs.HasKey("ParentToken"))
            {
                var arrayJwt = PlayerPrefs.GetString("ParentToken").Split(',');

                // определяем позицию ребенка 
                var lat = 0;
                var lng = 0;

                foreach (var jwt in arrayJwt)
                {
                    SendPointChildren(jwt, lat, lng);
                }
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

    private IEnumerator GetGeolocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        // Input.location.Stop();
    }


}
