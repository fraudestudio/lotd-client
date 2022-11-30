using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Security;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public static class Server
{



    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://info-dij-sae001.iut21.u-bourgogne.fr"),
    };



    public static bool VerifyUser(string id, string password)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;
        bool b = false;

        string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(id+":"+password));

        var content = new StringContent("");

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

        var response = sharedClient.PostAsync("api/account/signin", content);


        string[] jsonstring = response.Result.ToString().Split(',');

        foreach (string str in jsonstring)
        {
            if (str.Contains("ReasonPhrase: 'OK'"))
            {
                b = true;
            }
        }


        return b;
    }


    public static bool UserHasUniver()
    {

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetAsync("api/universe/all");

        string json = response.Result.ToString();

        Debug.Log(json);

        return false;
        
    }


}
