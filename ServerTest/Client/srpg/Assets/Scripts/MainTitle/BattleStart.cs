using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class BattleStart : MonoBehaviour {
    public int nextscene_num;
    public bool stage_lock = false;
    public Sprite rock;
    public Sprite unrock;
    public int stagenum;
	// Use this for initialization
    void Awake()
    {
      
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (StageManager.Getinst().cur_stage >= stagenum)
            stage_lock = true;
        if (stage_lock == false)
            GetComponent<SpriteRenderer>().sprite = rock;
        else
            GetComponent<SpriteRenderer>().sprite = unrock;
    }
    void OnMouseDown()
    {
        if (stage_lock == true)
        {
            SoundManager.GetInst().PlayClickSound();
            SceneManager.LoadScene(nextscene_num);
        }
    }
}
