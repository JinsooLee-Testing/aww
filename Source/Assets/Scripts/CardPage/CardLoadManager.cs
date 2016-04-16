using UnityEngine;
using System.Collections;

public class CardLoadManager : MonoBehaviour {
    private static CardLoadManager inst = null;
    public GameObject GO_hex;
    public GameObject GO_hex2;
    public GameObject GO_hex3;
    public int MapSizeX;
    public int MapSizeY;

    public float HexW; //Awake에서 설정
    public float HexH; //Awake

    CardBase[][] Map;
    // Use this for initialization
    public static CardLoadManager GetInst()
    {
        return inst;
    }
    void awake()
    {
        inst = this;
        SetCardSize();
    }
    void SetCardSize()
    {
        HexW = GO_hex.GetComponent<Renderer>().bounds.size.x;
        HexH = GO_hex.GetComponent<Renderer>().bounds.size.z;
    }
    public Vector3 GetWorldPos(int x, int y)
    {
        float X, Y;
        X = x * HexW;
        Y = y * HexH;
        return new Vector3(X, Y, 0);
    }
    public void LoadCard()
    {

        Map = new CardBase[MapSizeX + 1][];
        for (int x = 0; x <= MapSizeX; x++)
        {
            Map[x] = new CardBase[MapSizeY + 1];
            for (int y = 0; y <= MapSizeY; y++)
            {
                if(x%2==0)
                     Map[x][y] = ((GameObject)Instantiate(GO_hex)).GetComponent<CardBase>();
                else if(y % 2!=0)
                    Map[x][y] = ((GameObject)Instantiate(GO_hex2)).GetComponent<CardBase>();
                else
                    Map[x][y] = ((GameObject)Instantiate(GO_hex3)).GetComponent<CardBase>();
                Vector3 pos2 = GetWorldPos(x,  y);
                    Map[x][y].transform.position = pos2;
                    Map[x][y].SetMapPos(x, y);
            }
        }
    }
    void Start () {
        LoadCard();

    }
	
	void Update () {
	
	}
}
