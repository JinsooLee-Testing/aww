using UnityEngine;
using System.Collections;

public class ui_count : MonoBehaviour {

    bool cnt = false;
   // public string max = "/";
    public string count = "0";
    public int cnt_max;
	// Use this for initialization
	void Start () {
       
    }
	// Update is called once per frame
	void Update () {
        if (cnt == false)
        {
            string temp = PlayerManager.GetInst().EnemyCount.ToString();
            cnt_max = PlayerManager.GetInst().EnemyCount;
            //max += temp;
            cnt = true;
        }

        count = (PlayerManager.GetInst().EnemyCount).ToString();
        GetComponent<TextMesh>().text = count;
	}
}
