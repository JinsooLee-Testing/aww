using UnityEngine;
using System.Collections;

public class StageButton : MonoBehaviour {

    void Start()
    {
        Printphp();
        Debug.Log(StaticData.userStage);
        if(StaticData.userStage == 0)
        {
            GameObject.Find("Stage1Lock").SetActive(false);
            GameObject.Find("Stage1UnLock").SetActive(true);
            GameObject.Find("Stage2Lock").SetActive(true);
            GameObject.Find("Stage2UnLock").SetActive(false);
            GameObject.Find("Stage3Lock").SetActive(true);
            GameObject.Find("Stage3UnLock").SetActive(false);
        }
        else if (StaticData.userStage == 1)
        {
            GameObject.Find("Stage1Lock").SetActive(false);
            GameObject.Find("Stage1UnLock").SetActive(true);
            GameObject.Find("Stage2Lock").SetActive(false);
            GameObject.Find("Stage2UnLock").SetActive(true);
            GameObject.Find("Stage3Lock").SetActive(true);
            GameObject.Find("Stage3UnLock").SetActive(false);

        }
        else if (StaticData.userStage >= 2)
        {
            GameObject.Find("Stage1Lock").SetActive(false);
            GameObject.Find("Stage1UnLock").SetActive(true);
            GameObject.Find("Stage2Lock").SetActive(false);
            GameObject.Find("Stage2UnLock").SetActive(true);
            GameObject.Find("Stage3Lock").SetActive(false);
            GameObject.Find("Stage3UnLock").SetActive(true);
        }

    }

    IEnumerator Printphp()
    {

        string url = "http://localhost/select_stage.php";
        // string url = "http://localhost/Aww/login.php";
        //WWWForm form = new WWWForm();
        //form.AddField("ID", StaticData.userStage);
        //form.AddField("PASSWD", pass.text);

        WWW www = new WWW(url);

        yield return www;


        if (null == www.error)
        {
            if ("0" == www.text)
            {
                StaticData.userStage = 0;
                Application.LoadLevel("main_scene");
            }
            else if ("1" == www.text)
            {
                StaticData.userStage = 1;
                Application.LoadLevel("main_scene");
            }
            else if ("2" == www.text)
            {
                StaticData.userStage = 2;
                Application.LoadLevel("main_scene");
            }
            if ("3" == www.text)
            {
                StaticData.userStage = 3;
                Application.LoadLevel("main_scene");
            }
        }
        else
        {
            Debug.Log(www.error);
        }

    }


}
