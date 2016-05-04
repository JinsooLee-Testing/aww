using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class BattleStart : MonoBehaviour {
    public int nextscene_num;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        SoundManager.GetInst().PlayClickSound();
        SceneManager.LoadScene(nextscene_num);
    }
}
