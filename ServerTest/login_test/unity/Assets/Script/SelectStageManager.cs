using UnityEngine;
using System.Collections;

public class SelectStageManager : MonoBehaviour {

    //Login ldata = new Login();
    //PublicData data = new PublicData();
    void Awake()
    {
        Debug.Log(StaticData.userStage);
       //Debug.Log(ldata.data.getStage());
    }
    //Debug.Log("111");
    // 현재 스테이지 정보를 로드한다. 
    public void LoadCStage()
    {
        Application.LoadLevel("C_Stage");

    }

    public void LoadYStage()
    {
        Application.LoadLevel("Y_Stage");

    }

    public void LoadMStage()
    {
        Application.LoadLevel("M_Stage");

    }
    public void LoadBack()
    {
        Application.LoadLevel("main_scene");

    }
}
