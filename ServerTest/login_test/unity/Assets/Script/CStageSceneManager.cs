using UnityEngine;
using System.Collections;

public class CStageSceneManager : MonoBehaviour {

    //void Start()
    //{
    //    //if (0==StaticData.userStage)
            
    //}
    // Use this for initialization
    public void ClearStage()
    {
        if (0 == StaticData.userStage)
        {
            StaticData.userStage = 1;
            StartCoroutine(Printphp());
        }
    }
    public void LoadNextStage()
    {
        Debug.Log("LoadNextStage: " + StaticData.userStage);
        if (1 <= StaticData.userStage)
        {
            Debug.Log("LoadNextStage if : " + StaticData.userStage);
            Application.LoadLevel("Y_Stage");
        }      
    }

    public void LoadBack()
    {
        Application.LoadLevel("stage_sel_scene");

    }
    IEnumerator Printphp()
    { 
        Debug.Log("Printphp : " +StaticData.userId+","+StaticData.userPasswd+"," +StaticData.userStage);
        string url = "http://localhost/changeStage.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", StaticData.userId);
        form.AddField("PASSWD", StaticData.userPasswd);
        form.AddField("STAGE",StaticData.userStage);
        WWW www = new WWW(url, form);

        yield return www;


        if (null == www.error)
        {
            Debug.Log(www.text);
            //if("1"==www.text)
            //{
            //    //StaticData.userStage = 1;
            //}
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
