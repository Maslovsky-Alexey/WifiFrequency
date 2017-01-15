using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class GetFreq : MonoBehaviour
{
    public string Pattern;
    public Text Text;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        try
        {
            var wifi = new AndroidJavaClass("android.net.wifi.WifiInfo");
            
            var ssid = "";
            var freq = "";

            try
            {
                ssid = get<string>("getSSID").ToString();
            }
            catch (Exception e)
            {
                ssid = e.Message;
            }

            try
            {
                freq = get<int>("getRssi").ToString();
            }
            catch (Exception e)
            {
                freq = e.Message;
            }


            Text.text = string.Format(Pattern, ssid, freq);
        }
        catch(Exception e)
        {

            Text.text = e.Message;
        }

	}

    public static object get<T>(string name)
    {
        object tempSSID = "";

        try
        {
            using (var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi"))
                {
                    tempSSID = wifiManager.Call<AndroidJavaObject>("getConnectionInfo").Call<T>(name);
                }
            }
        }
        catch (Exception e)
        {
            tempSSID = e.Message;
        }

        return tempSSID;
    }
}
