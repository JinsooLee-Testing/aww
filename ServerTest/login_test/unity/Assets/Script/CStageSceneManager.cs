using UnityEngine;
using System.Collections;

public class CStageSceneManager : MonoBehaviour {

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
