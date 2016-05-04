using UnityEngine;
using System.Collections;

public class YStageSceneManager : MonoBehaviour {

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
