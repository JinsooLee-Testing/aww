using UnityEngine;
using System.Collections;

public class SelectStageManager : MonoBehaviour {

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
