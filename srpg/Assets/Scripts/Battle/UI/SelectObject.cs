using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {
   public Sprite DENTI;
    public Sprite Obj;
    public Sprite chick;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        PlayerBase pb = PlayerManager.GetInst().select_object;
        if (pb.m_type == Type.MAINCHARACTER)
        {
            GetComponent<SpriteRenderer>().sprite = DENTI;
            PlayerManager.GetInst().select_object = pb;
           
        }
        else if (pb.m_type == Type.USER)
        {
            GetComponent<SpriteRenderer>().sprite = Obj;
            PlayerManager.GetInst().select_object = pb;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = chick;
            PlayerManager.GetInst().select_object = pb;
        }
    }
    
}
