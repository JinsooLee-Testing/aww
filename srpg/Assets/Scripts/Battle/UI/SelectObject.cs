using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {
   public Sprite DENTI;
    public Sprite Obj;
    public Sprite chick;
    public Sprite bunny;
    public Sprite gorilra;
    public Sprite golem;
   
    void Awake()
    {
    }
    // Use this for initialization
    void Start () {
  
    }

    // Update is called once per frame
    void Update()
    {
        if(GUIManager.GetInst().talkmode)
        {
           
           if (GUIManager.GetInst().named=="bunny")
              GetComponent<SpriteRenderer>().sprite = bunny;
           else
                GetComponent<SpriteRenderer>().sprite = DENTI;
        }
        if (GUIManager.GetInst().talkscene == false)
        {
            if (CameraManager.GetInst().event_mode == false && GUIManager.GetInst().talkmode == false)
            {

                PlayerBase pb = PlayerManager.GetInst().select_object;
                if (pb.m_type == Type.MAINCHARACTER)
                {
                    GetComponent<SpriteRenderer>().sprite = DENTI;

                }
                else if (pb.m_type == Type.USER)
                {
                    GetComponent<SpriteRenderer>().sprite = Obj;
                }
                else
                {
                    if (pb.Monster_id == 1)
                        GetComponent<SpriteRenderer>().sprite = chick;
                    if (pb.Monster_id == 2)
                        GetComponent<SpriteRenderer>().sprite = bunny;
                    if (pb.Monster_id == 3)
                        GetComponent<SpriteRenderer>().sprite = gorilra;
                    if (pb.Monster_id == 4)
                        GetComponent<SpriteRenderer>().sprite = Obj;
                    if (pb.Monster_id == 5)
                        GetComponent<SpriteRenderer>().sprite = golem;

                }
            }
        }
    }
    
}
