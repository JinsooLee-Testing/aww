using UnityEngine;
using System.Collections;

public class costBase : MonoBehaviour {
    public Sprite sp_image;
    public Sprite sp_image2;
    bool is_Empty = true;

    public void SetEmpty(bool emt)
    {
        is_Empty = emt;
        if (is_Empty==true)
         GetComponent<SpriteRenderer>().sprite = sp_image2;
        else
        {
            GetComponent<SpriteRenderer>().sprite = sp_image;
        }
    }
    // Use this for initialization
    void Start () {
        if (is_Empty == true)
            GetComponent<SpriteRenderer>().sprite = sp_image2;
        else
        {
            GetComponent<SpriteRenderer>().sprite = sp_image;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
