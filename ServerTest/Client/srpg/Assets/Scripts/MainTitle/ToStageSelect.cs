using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ToStageSelect : MonoBehaviour {

    public int sceennum;
    public float removeTime = 0;
    void Start()
    {
      
    }
    public void Stage_select()
    {
        // FIleManager.Getinst().SaveCardData();
        SoundManager.GetInst().PlayClickSound();
        SceneManager.LoadScene(6);
    }
    public void card_select()
    {
        // FIleManager.Getinst().SaveCardData();
        SoundManager.GetInst().PlayClickSound();
        SceneManager.LoadScene(1);
    }
    public void getSound(string name)
    {
        switch(name)
        {
            case "btn01_onClick":
                break;
        }
    }
}

