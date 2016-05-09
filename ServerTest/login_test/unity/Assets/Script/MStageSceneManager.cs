using UnityEngine;
using System.Collections;

public class MStageSceneManager : MonoBehaviour {

    void Start()
    {
        if (2 == StaticData.userStage)
        {
            StaticData.userStage += 1;
        }
    }
    public void LoadBack()
    {
        Application.LoadLevel("stage_sel_scene");

    }
}
