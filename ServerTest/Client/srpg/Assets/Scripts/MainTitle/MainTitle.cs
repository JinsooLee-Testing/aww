using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainTitle : MonoBehaviour {

    public int sceennum;
    public float removeTime=0;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (removeTime != 0)
        {
            removeTime += Time.deltaTime;
            if (removeTime >= 5.0f)
            {
                SceneManager.LoadScene(sceennum);

            }
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
                return;
            }
        }
    }
    void OnMouseDown()
    {
       // FIleManager.Getinst().SaveCardData();
        //SoundManager.GetInst().PlayClickSound();
        SceneManager.LoadScene(sceennum);
    }
}
