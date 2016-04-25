using UnityEngine;
using System.Collections;


public class BattleCardManager : MonoBehaviour
{
    private static BattleCardManager inst = null;
    public GameObject GO_hex;

    public int MapSizeX;
    public int MapSizeY;

    public float HexW; //Awake에서 설정
    public float HexH; //Awake
    public Vector3 Initpos;

    CardBase[] card;
    // Use this for initialization
    public static BattleCardManager GetInst()
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

        card = new CardBase[MapSizeX + 1];
        for (int x = 0; x <= MapSizeX; x++)
        {

            card[x] = ((GameObject)Instantiate(GO_hex)).GetComponent<CardBase>();
            card[x].transform.position = Initpos;
            Initpos.x += 5;
        }
    }
    void Start()
    {
        LoadCard();

    }

    void Update()
    {
       
    }
}
