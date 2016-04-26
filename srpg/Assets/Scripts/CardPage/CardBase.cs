using UnityEngine;
using System.Collections;

public class CardBase : MonoBehaviour {


    public Sprite sp_image;
    public Sprite sp_image2;
   
    protected bool  On_active = true;
    ACT act;
    // public GameObject GO_Card;
  
    string type;
    //public Point MapPos;
    // Use this for initialization
    void Awake()
    {
    }
    void Start () {
        GetComponent<SpriteRenderer>().sprite = sp_image;
    }
	// Update is called once per frame
	void Update () {

        
    }

    // Use this for initialization
    public void SetMapPos(int x, float y)
    {
       // MapPos = new Point(x, y, 0);
    }
    void OnMouseDown()
    {
        if (CostManager.GetInst().cur_cost_num >= 3)
        {
            if (On_active == true)
            {
                PlayerManager.GetInst().HilightSummons();
                act = ACT.SUMMONES;
                Debug.Log("card");

                GetComponent<SpriteRenderer>().sprite = sp_image2;
                On_active = false;
                CostManager.GetInst().CostDecrease(3);
            }
        }
        //magic.GetInst().fire = true;
    }

}
