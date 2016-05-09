using UnityEngine;
using System.Collections;

public class CStageSceneManager : MonoBehaviour {

    void Start()
    {
        if (0==StaticData.userStage) StaticData.userStage += 1;

    }
    // Use this for initialization
    public void LoadNextStage()
    {
        Application.LoadLevel("Y_Stage");

    }

    public void LoadBack()
    {
        Application.LoadLevel("stage_sel_scene");

    }
}
