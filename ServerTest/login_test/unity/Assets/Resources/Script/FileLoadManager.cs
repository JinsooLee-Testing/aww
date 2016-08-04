using UnityEngine;
using System.Collections;

public class FileLoadManager : MonoBehaviour {
    private static FileLoadManager inst = null;
    GameObject[] mCards = new GameObject[100];
    Quaternion mRot = Quaternion.Euler(0, 90, 0);
	// Use this for initialization
	void Awake () {
        inst = this;
        inst.mCards[0]=(GameObject)Resources.Load("CardPrefab/bunny");
        inst.mCards[1] = (GameObject)Resources.Load("CardPrefab/gorilla");
        inst.mCards[2] = (GameObject)Resources.Load("CardPrefab/mole");
       // card = (GameObject)Resources.Load("CardPrefab/None");
        Spawn();

    }
	public static FileLoadManager GetInst()
    {
        return inst;
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn()
    {
       Instantiate(inst.mCards[0], new Vector3(0,0, 0), mRot);
       Instantiate(inst.mCards[1], new Vector3(-5, 0, 0), mRot);
       Instantiate(inst.mCards[2], new Vector3(5, 0, 0), mRot);
       // Instantiate(card, new Vector3(0,0,0), Quaternion.identity);
       //card.SetActive(true);
    }
}
