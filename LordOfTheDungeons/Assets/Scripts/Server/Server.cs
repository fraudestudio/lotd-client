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
using UnityEngine.Rendering;
using System.Linq;
using Assets.Scripts.Server;
using static UnityEngine.Networking.UnityWebRequest;
using Mono.Cecil.Mdb;

public static class Server
{



    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://info-dij-sae001.iut21.u-bourgogne.fr:8443"),
    };


    /// <summary>
    /// Vérifie si les identifiants et mot de passe sont correctes pour se connecter
    /// </summary>
    /// <param name="id"></param>
    /// <param name="password"></param>
    /// <returns></returns>
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




    public static List<UniverseInfo> GetAllUniverses()
    {

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync("api/universe/all");

        List<UniverseInfo> json = JsonConvert.DeserializeObject<List<UniverseInfo>>(response.Result.ToString());

        return json;
    }


    public static List<UniverseInfo> GetUserJoinedUniverses()
    {

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync("api/universe/joined");

        List<UniverseInfo> json = JsonConvert.DeserializeObject<List<UniverseInfo>>(response.Result.ToString());

        return json;
    }


    public static bool UserHasUniverse()
    {

        bool result = false;

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync("api/universe/owned");


        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());


        if (json.Id != null)
        {
            result = true;
        }

        return result;
        
    }

    public static UniverseInfo GetUserUniverse()
    {

        UniverseInfo result = null;

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync("api/universe/owned");

        //string json = response.Result.ToString();



        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());

        if (json.Id != "null")
        {
            result = json;
        }

        return json;

    }


    public static bool UserHasVillageInUniverse(int universeID)
    {

        bool result = false;

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync(String.Format("api/universe/{0}", universeID));


        if (response.Result.ToString().Contains("Town"))
        {
            result = true;
        }


        return result;

    }

    public static UniverseInfo UserGetVillageID(int universeID)
    {

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync(String.Format("api/universe/{0}", universeID));

        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());


        return json;

    }


    public static UniverseInfo UserGetVillageName(int villageID)
    {

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync(String.Format("api/village/name/{0}", villageID));

        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());


        return json;

    }



    public static string UniverseGetMajorFaction(int universeID)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync(String.Format("api/universe/faction/{0}", universeID));

        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());

        return json.Faction;
    }



    public static int UniverseCountVillage(int universeID)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456789012345678901234567890");

        var response = sharedClient.GetStringAsync(String.Format("api/universe/count/{0}", universeID));

        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());


        return json.NumberVillage;
    }




    public static void CreateUniverse(string name, string mdp)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        bool pswd = false;

        if (mdp != null)
        {
            pswd = true;
        }

        UniverseInfo info = new UniverseInfo
        {
            Name = name,
            HasPassword = pswd,
            Password = mdp
        };


        var response = sharedClient.PostAsync("api/universe/create", new StringContent(JsonConvert.SerializeObject(info),Encoding.UTF8, "application/json"));

        Debug.Log(response.Result.ToString());
    }

}
