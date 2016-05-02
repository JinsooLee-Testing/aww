using UnityEngine;
using System.Collections;

public class CardBase : CardUseBase {

    
    public Sprite sp_image;
    public Sprite sp_image2;
    public TextMesh text;
  

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
    public void SetCost(int cos)
    {
        text.text = cos.ToString();
    }
	// Update is called once per frame
	void Update () {
        if (BattleCardManager.GetInst().cardUse[Buttonnum].On_active == false)
            On_active = false;
        else
            On_active = true;
        if (On_active == false)
        {
            GetComponent<SpriteRenderer>().sprite = sp_image2;
        }
        else
            GetComponent<SpriteRenderer>().sprite = sp_image;
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
