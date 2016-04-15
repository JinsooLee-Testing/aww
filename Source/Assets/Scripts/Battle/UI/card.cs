using UnityEngine;
using System.Collections;

public class card : MonoBehaviour {
    GUIManager gm;
   
    public GameObject GO_Card;
    string act = "Idle";
    // Use this for initialization
    void Awake()
    {
        gm = GUIManager.GetInst();
    }
    void Start () {
         
    }
	// Update is called once per frame
	void Update () {      
        if(act=="put")
        {
            Vector3 v = transform.position;
            v.y += 15;
            transform.position += (v - transform.position).normalized  * 5*Time.smoothDeltaTime;
            
        }
        
    }
    void OnMouseDown()
    {
        PlayerManager pm = PlayerManager.GetInst();
        Debug.Log("card");
        act = "put";
        pm.GenPlayer(0, 1);

    }

}
