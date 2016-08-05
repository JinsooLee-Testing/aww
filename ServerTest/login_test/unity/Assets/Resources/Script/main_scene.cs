using UnityEngine;
using System.Collections;

public class main_scene : MonoBehaviour {

    //public int stageNum;
    //public int id;
    public void LoadStage()
    {
        Application.LoadLevel("stage_sel_scene");
       
    }

    public void LoadCard()
    {
        Application.LoadLevel("card_sel_scene");
        
    }

}
