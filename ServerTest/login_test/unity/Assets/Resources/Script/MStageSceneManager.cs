using UnityEngine;
using System.Collections;

public class MStageSceneManager : MonoBehaviour {


    public void ClearStage()
    {
        if (2 == StaticData.userStage)
        {
            StaticData.userStage = 3;
            StartCoroutine(Printphp());
        }
    }
    public void LoadBack()
    {
        Application.LoadLevel("stage_sel_scene");

    }

    IEnumerator Printphp()
    {
        Debug.Log("Printphp : " + StaticData.userId + "," + StaticData.userPasswd + "," + StaticData.userStage);
        string url = "http://localhost/changeStage.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", StaticData.userId);
        form.AddField("PASSWD", StaticData.userPasswd);
        form.AddField("STAGE", StaticData.userStage);
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
