using UnityEngine;
using System.Collections;

public class StageButton : MonoBehaviour {

    void Start()
    {
        Debug.Log(StaticData.userStage);
        if(StaticData.userStage == 0)
        {
            GameObject.Find("Stage1Lock").SetActive(false);
            GameObject.Find("Stage1UnLock").SetActive(true);
        }
        else if (StaticData.userStage == 1)
        {
            GameObject.Find("Stage1Lock").SetActive(false);
            GameObject.Find("Stage1UnLock").SetActive(true);
            GameObject.Find("Stage2Lock").SetActive(false);
            GameObject.Find("Stage2UnLock").SetActive(true);

        }
        else if (StaticData.userStage >= 2)
        {
            GameObject.Find("Stage1Lock").SetActive(false);
            GameObject.Find("Stage1UnLock").SetActive(true);
            GameObject.Find("Stage2Lock").SetActive(false);
            GameObject.Find("Stage2UnLock").SetActive(true);
            GameObject.Find("Stage3Lock").SetActive(false);
            GameObject.Find("Stage3UnLock").SetActive(true);
        }

    }



	
}
