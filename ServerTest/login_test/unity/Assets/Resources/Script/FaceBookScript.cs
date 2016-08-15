using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.Unity;
public class FaceBookScript : MonoBehaviour {

    public GameObject LoggedInUI;
    public GameObject NotLoggedInUI;
   // public GameObject Friend;

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack);
        }
    }

    void InitCallBack()
    {
        Debug.Log("FB has been initiased.");
        ShowUI();
    }

    public void Login()
    {
        if (!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string> { "user_friends" }, LoginCallBack);
        }
    }

    void LoginCallBack(ILoginResult result)
    {
        if (result.Error == null)
        {
            Debug.Log("FB has logged in.");
            ShowUI();
        }
        else {
            Debug.Log("Error during login: " + result.Error);
        }
    }

    void ShowUI()
    {
        if (FB.IsLoggedIn)
        {
            LoggedInUI.SetActive(true);
            NotLoggedInUI.SetActive(false);
            FB.API("me/picture?width=100&height=100", HttpMethod.GET, PictureCallBack);
            FB.API("me", HttpMethod.GET, NameCallBack);
            //FB.API("me/friends", HttpMethod.GET, FriendCallBack);
        }
        else {
            LoggedInUI.SetActive(false);
            NotLoggedInUI.SetActive(true);
        }
    }

    void PictureCallBack(IGraphResult result)
    {
        Texture2D image = result.Texture;
        LoggedInUI.transform.FindChild("ProfilePicture").GetComponent<Image>().sprite = Sprite.Create(image, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
    }

    void NameCallBack(IGraphResult result)
    {
        IDictionary<string, object> profile = result.ResultDictionary;
        LoggedInUI.transform.FindChild("Name").GetComponent<Text>().text = "Hello " + profile["name"];
        //Debug.Log(profile["id"]);
        StartCoroutine(Printphp(profile));
    }

    public void LogOut()
    {
        FB.LogOut();
        ShowUI();
    }
    IEnumerator Printphp(IDictionary<string, object> profile)
    {
        string id =  profile["id"].ToString();
        string name = profile["name"].ToString();
        //Debug.Log(id);
        //Debug.Log(name);
        string url = "http://52.78.5.177/AwwLogin.php";
        //string url = "http://localhost/Aww/Awwlogin.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", id);
        form.AddField("NAME", name);

        WWW www = new WWW(url,form);

        yield return www;


        if (null == www.error)
        {
            Debug.Log(www.text);
            //StaticData.userId = id.text;
            //StaticData.userPasswd = pass.text;

            Debug.Log(www.text);
            if ("Success" == www.text)
            {
                Application.LoadLevel("main_scene");
            }
            else
            {
                Debug.Log("Login fail");
            }
        }
        else
        {
            Debug.Log(www.error);
        }

    }

}
