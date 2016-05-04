using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainTitle : MonoBehaviour {

    public int sceennum;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
       // FIleManager.Getinst().SaveCardData();
        //SoundManager.GetInst().PlayClickSound();
        SceneManager.LoadScene(sceennum);
    }
}
