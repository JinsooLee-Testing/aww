using UnityEngine;
using System.Collections;


public class CardLoadManager : MonoBehaviour
{
    private static CardLoadManager inst = null;
    public GameObject GO_hex;
    public GameObject GO_SUMMON;
    public GameObject GO_water;
    public GameObject GO_bunny;
    public GameObject GO_Fire;
    public int MapSizeX;
    public int MapSizeY;

    public float HexW; //Awake에서 설정
    public float HexH; //Awake
    public Vector3 Initpos;
    public Vector3 Initpos2;

    CardBase[][] card;
    CardUseBase[][] cardUse;
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
    public void Fire()
    {


    }
    public void LoadCard()
    {

        card = new CardBase[MapSizeX+ 1][];
        for (int x = 0; x <= MapSizeX; x++)
        {
            card[x] = new CardBase[MapSizeY + 1];
            for (int y = 0; y <= MapSizeY; y++)
            {
                float X = x * HexW;
                float Y = y * HexH;
                Vector3 v= new Vector3(X, 0, Y);
                card[x][y] = ((GameObject)Instantiate(GO_hex)).GetComponent<CardBase>();
                card[x][y].transform.position = v;

                Vector3 r = new Vector3(90, 0, 0);
                card[x][y].transform.rotation = Quaternion.Euler(r);
                card[x][y].Buttonnum = x;
               
            
                
            }
           
        }

        cardUse = new CardUseBase[MapSizeX + 1][];
        for (int x = 0; x <= MapSizeX; x++)
        {
            cardUse[x] = new CardUseBase[MapSizeY + 1];
            for (int y = 0; y <= MapSizeY; y++)
            {

                if (x == 0)
                    cardUse[x][y] = ((GameObject)Instantiate(GO_SUMMON)).GetComponent<SummonCard>();
                else if(x==1)
                    cardUse[x][y] = ((GameObject)Instantiate(GO_bunny)).GetComponent<SummonCard>();
                else if(x==2)
                    cardUse[x][y] = ((GameObject)Instantiate(GO_water)).GetComponent<MagicCard>();
                else
                    cardUse[x][y] = ((GameObject)Instantiate(GO_Fire)).GetComponent<MagicCard>();
                float X = x * HexW;
                float Y = y * HexH;
                Vector3 v = new Vector3(X, 0.3f, Y);
                cardUse[x][y].transform.position = v;

                Vector3 r = new Vector3(90, 0, 0);
                cardUse[x][y].transform.rotation = Quaternion.Euler(r);
                cardUse[x][y].Buttonnum = x;



            }

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
