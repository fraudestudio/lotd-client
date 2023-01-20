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
using System.Security.Cryptography;
using Assets.Scripts.Server.Menu;
using Assets.Scripts.Server.Model;
using Assets.Scripts.Village;
using Assets.Scripts.Server.Model.Connexion;
using System.Net;

public static class Server
{

    private static int saveIdUniverse;
    private static int saveIdVillage;
    private static string name;

    public static string Name { get => name; }


    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://info-dij-sae001.iut21.u-bourgogne.fr:8443"),
    };


    /// <summary>
    /// V�rifie si les identifiants et mot de passe sont correctes pour se connecter
    /// </summary>
    /// <param name="id"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string VerifyUser(string id, string password)
    {
        string res = "";

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (s, ce, ca, p) => true;

        string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(id+":"+password));

        var content = new StringContent("");

        sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

        var response = sharedClient.PostAsync("api/account/signin", content);

        SignInSuccess json = JsonConvert.DeserializeObject<SignInSuccess>(response.Result.Content.ReadAsStringAsync().Result);


        if (response.Result.StatusCode == HttpStatusCode.OK)
        {
            if (json.Validated)
            {
                name = id;
                res = "Success";
                sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", json.SessionToken);
                GameServer.Instance.Token = json.SessionToken;
            }
            else
            {
                res = "NotValid";
            }

        }
        else
        {
            res = "Error";
        }

        return res;
    }




    public static List<UniverseInfo> GetAllUniverses()
    {
        var response = sharedClient.GetStringAsync("api/universe/all");

        List<UniverseInfo> json = JsonConvert.DeserializeObject<List<UniverseInfo>>(response.Result.ToString());

        return json;
    }


    public static List<UniverseInfo> GetUserJoinedUniverses()
    {

        var response = sharedClient.GetStringAsync("api/universe/joined");

        List<UniverseInfo> json = JsonConvert.DeserializeObject<List<UniverseInfo>>(response.Result.ToString());

        return json;
    }


    public static bool UserHasUniverse()
    {

        bool result = false;

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

        var response = sharedClient.GetStringAsync("api/universe/owned");

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

        var response = sharedClient.GetStringAsync(String.Format("api/universe/{0}", universeID));


        if (response.Result.ToString().Contains("Town"))
        {
            result = true;
        }


        return result;

    }

    public static UniverseInfo UserGetVillageID(int universeID)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/universe/{0}", universeID));

        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());


        return json;

    }


    public static UniverseInfo UserGetVillageName(int villageID)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/name", villageID));

        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());


        return json;

    }



    public static string UniverseGetMajorFaction(int universeID)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/universe/faction/{0}", universeID));

        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());

        return json.Faction;
    }



    public static int UniverseCountVillage(int universeID)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/universe/count/{0}", universeID));

        UniverseInfo json = JsonConvert.DeserializeObject<UniverseInfo>(response.Result.ToString());


        return json.NumberVillage;
    }




    public static void CreateUniverse(string name, string mdp)
    {

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
    }

    public static void CreateVillage(string name, string race, int idUnivers)
    {

        VillageInfo info = new VillageInfo
        {
            Name=name,
            Faction = race,
            IdUnivers = idUnivers
        };


        var response = sharedClient.PostAsync("api/village/create", new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json"));
    }


    public static bool VerifyAcessUniverse(int idUniverse, string password)
    {

        bool result = false;

        var content = new { Password = password };

        var response = sharedClient.PostAsync(String.Format("api/universe/access/{0}",idUniverse), new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));


        if (response.Result.Content.ReadAsStringAsync().Result == "Success")
        {
            result = true;
        }


        return result;

    }

    public static bool InitVillage(int idVillage)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/init", idVillage));

        bool json = JsonConvert.DeserializeObject<bool>(response.Result.ToString());


        return json;
    }

    public static Dictionary<string,int> GetLevelVillage()
    {

        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/level", GetCurrentVillage()));

        Dictionary<string,int> json = JsonConvert.DeserializeObject<Dictionary<string,int>>(response.Result.ToString());

        return json;
    }


    public static bool GetInConstructionVillage(string building)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/{1}/construction/get", GetCurrentVillage(),building));

        bool json = JsonConvert.DeserializeObject<bool>(response.Result.ToString());

        return json;
    }

    public static bool SetInConstructionVillage(string building)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/{1}/construction/set", GetCurrentVillage(), building));

        bool json = JsonConvert.DeserializeObject<bool>(response.Result.ToString());

        return json;
    }

    public static int GetInConstructionVillageTime(string building)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/{1}/construction/gettime", GetCurrentVillage(), building));

        int json = JsonConvert.DeserializeObject<int>(response.Result.ToString());

        return json;
    }

    public static bool LevelUpBuilding(string building)
    {

        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/{1}/construction/up", GetCurrentVillage(), building));

        bool json = JsonConvert.DeserializeObject<bool>(response.Result.ToString());

        return json;
    }

    public static Ressources GetRessources()
    {

        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/ressources/get", GetCurrentVillage()));

        Ressources json = JsonConvert.DeserializeObject<Ressources>(response.Result.ToString());

        return json;
    }


    public static bool SetRessources(int wood, int stone, int gold)
    {
        bool res = false;


        Ressources r = new Ressources
        {
            Bois = wood,
            Pierre = stone,
            Or = gold
        };

        var response = sharedClient.PostAsync(String.Format("api/village/{0}/ressources/set", GetCurrentVillage()), new StringContent(JsonConvert.SerializeObject(r), Encoding.UTF8, "application/json"));


        if (response.Result.Content.ReadAsStringAsync().Result == "true")
        {
            res = true;
        }

        return res;
    }

    public static bool DeleteAllCharacterFromTaverne(int idVillage)
    {
        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/taverne/delete_all", GetCurrentVillage()));

        bool json = JsonConvert.DeserializeObject<bool>(response.Result.ToString());

        return json;
    }

    public static bool SetStartTimeTaverneNow(int idVillage)
    {
        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/taverne/set/time_batiment", GetCurrentVillage()));

        bool json = JsonConvert.DeserializeObject<bool>(response.Result.ToString());

        return json;
    }

    public static int GetStartTimeTaverne(int idVillage)
    {
        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/taverne/get/time_batiment",idVillage));

        Debug.Log(response.Result.ToString());

        int json = JsonConvert.DeserializeObject<int>(response.Result.ToString());

        return json;
    }

    public static Equipement getEquipement(int idPersonnage)
    {
        var reponse = sharedClient.GetStringAsync(String.Format("api/village/character/equipement/get/{0}", idPersonnage));

        Equipement json = JsonConvert.DeserializeObject<Equipement>(reponse.Result.ToString());

        return json;
    }

    public static int InitCharacter(int idVillage)
    {
        var reponse = sharedClient.GetStringAsync(String.Format("api/village/{0}/character/create", idVillage));

        int json = JsonConvert.DeserializeObject<int>(reponse.Result.ToString());

        return json;
    }

    public static CharacterModel GetCharacterByID(int idCharacter)
    {
        var response = sharedClient.GetStringAsync(String.Format("api/village/character/get/{0}",idCharacter));

        CharacterModel json = JsonConvert.DeserializeObject<CharacterModel>(response.Result.ToString());

        return json;
    }

    public static List<int> GetCharacterInBatiment(int idVillage, string batiment)
    {
        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/{1}/GetCharacterInBatiment", idVillage,batiment));

        Debug.Log(response.Result.ToString());

        List<int> json = JsonConvert.DeserializeObject<List<int>>(response.Result.ToString());

        return json;
    } 

    public static bool SetCharacterInBatiment(int idVillage, int idPersonnage, string batiment)
    {
        var response = sharedClient.GetStringAsync(String.Format("api/village/{0}/{1}/{2}/insertPersoInBatiment", idVillage, idPersonnage, batiment));

        bool json = JsonConvert.DeserializeObject<bool>(response.Result.ToString());

        return json;
    }

    public static int GoExpedition(int idVillage)
    {
       
        var response = sharedClient.PostAsync(String.Format("api/village/{0}/expedition", idVillage), new StringContent(JsonConvert.SerializeObject(new List<int>()), Encoding.UTF8, "application/json"));

        int json = JsonConvert.DeserializeObject<int>(response.Result.Content.ReadAsStringAsync().Result);

        return json;
    }

    public static void SaveIdUniverse(int value)
    {
        saveIdUniverse = value;
    }

    public static int GetSavedIdUniverse()
    {
        return saveIdUniverse;
    }

    public static int GetCurrentVillage()
    {
        return saveIdVillage;

    }

    public static void SetCurrentVillage(int value)
    {
        saveIdVillage = value;
    }

}
