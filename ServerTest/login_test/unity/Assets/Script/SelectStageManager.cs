using UnityEngine;
using System.Collections;

public class SelectStageManager : MonoBehaviour {

    public int stageNum=0;
    public int getStageNum()
    {
        return 0;
    }
    public void setStageNum(int inputNum)
    {
        stageNum = inputNum;
    }
    void Awake()
    {
       Debug.Log(stageNum);
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
