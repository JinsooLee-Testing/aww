using UnityEngine;
using System.Collections;

public class CardBase : MonoBehaviour {
   
   // public GameObject GO_Card;
    ACT act = ACT.IDLE;
    //public Point MapPos;
    // Use this for initialization
    void Awake()
    {
    }
    void Start () {
         
    }
	// Update is called once per frame
	void Update () {
        if (act == ACT.SUMMONES)
        {
            Vector3 v = transform.position;
            v.y += 20;
   
            transform.position += (v - transform.position).normalized * 6 * Time.smoothDeltaTime;
            Vector3 curpos = transform.position;
            if (curpos.y > 25)
            {
                PlayerManager.GetInst().HilightSummons();
                act = ACT.IDLE;
            }
        }
        
    }

    // Use this for initialization
    public void SetMapPos(int x, float y)
    {
       // MapPos = new Point(x, y, 0);
    }
    void OnMouseDown()
    {
        PlayerManager pm = PlayerManager.GetInst();
        act = ACT.SUMMONES;
        Debug.Log("card");
     
       

    }

}
