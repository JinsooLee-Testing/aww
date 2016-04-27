using UnityEngine;
using System.Collections;

public class CardBase : CardUseBase {

    
    public Sprite sp_image;
    public Sprite sp_image2;

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
        if (On_active == false)
        {
            GetComponent<SpriteRenderer>().sprite = sp_image2;
        }
    }

    // Use this for initialization
    public void SetMapPos(int x, float y)
    {
       // MapPos = new Point(x, y, 0);
    }
    void OnMouseDown()
    {

       
    }
        //magic.GetInst().fire = true;
    

}
