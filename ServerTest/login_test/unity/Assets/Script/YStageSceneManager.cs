using UnityEngine;
using System.Collections;

public class YStageSceneManager : MonoBehaviour {

    void Start()
    {
        if(1 == StaticData.userStage )
        {
            StaticData.userStage += 1;
        }
    }
    // Use this for initialization
    public void LoadNextStage()
    {
        Application.LoadLevel("M_Stage");

    }

    public void LoadBack()
    {
        Application.LoadLevel("stage_sel_scene");

    }
}
