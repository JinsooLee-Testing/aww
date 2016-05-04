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
    public string path;
    public int butnum = 1;
    public GameObject[] Cards = new GameObject[100];

    CardBase[][] card;
    public CardUseBase[][] cardUse;
    // Use this for initialization
    void Awake()
    {
        inst = this;
        //SetCardSize();
        inst.Cards[0] = (GameObject)Resources.Load("Prefabs/card/emptycard");
        inst.Cards[1] = (GameObject)Resources.Load("Prefabs/card/fire");
        inst.Cards[2] = (GameObject)Resources.Load("Prefabs/card/water");
        inst.Cards[3] = (GameObject)Resources.Load("Prefabs/card/wall");
        inst.Cards[4] = (GameObject)Resources.Load("Prefabs/card/summon2");
        inst.Cards[5] = (GameObject)Resources.Load("Prefabs/card/bunny");
        inst.Cards[6] = (GameObject)Resources.Load("Prefabs/card/croco");
        butnum = 0;
    }
    public static CardLoadManager GetInst()
    {
        return inst;
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
    public void OnCard(int id)
    {

        Debug.Log("fire");
        if (butnum > 3)
            butnum = 0;
        Vector3 temp = cardUse[butnum][2].transform.position;
        Quaternion temprot = cardUse[butnum][2].transform.rotation;
        Destroy(cardUse[butnum][2].gameObject);
        cardUse[butnum][2] = new CardUseBase();
        Debug.Log("onCard");
        if (id <= 3)
        {
            cardUse[butnum][2] = (MagicCard)GameObject.Instantiate(Cards[id]).GetComponent<MagicCard>();
        }
        else if (id > 3)
        {
            cardUse[butnum][2] = (SummonCard)GameObject.Instantiate(Cards[id]).GetComponent<SummonCard>();
        }

        cardUse[butnum][2].transform.position = temp;
        cardUse[butnum][2].transform.rotation = temprot;
        cardUse[butnum][2].Buttonnum = butnum;
        cardUse[butnum][2].On_active = true;
        cardUse[butnum][2].InGame = false;
        card[butnum][2].SetCost(cardUse[butnum][2].cost);
        cardUse[butnum][2].X = butnum;
        cardUse[butnum][2].Y = 2;
        cardUse[butnum][2].card_id = id;
        
        butnum++;
    }
    public void CreateXMLmap(CardsInfo info)
    {
        card = new CardBase[MapSizeX + 1][];
        for (int x = 0; x <= MapSizeX; x++)
        {
            card[x] = new CardBase[MapSizeY + 1];
            for (int y = 0; y <= MapSizeY; y++)
            {
                float X = x * HexW;
                float Y = y * HexH;
                Vector3 v = new Vector3(X, 0, Y);
                card[x][y] = ((GameObject)Instantiate(GO_hex)).GetComponent<CardBase>();

                card[x][y].transform.position = v;

                Vector3 r = new Vector3(90, 0, 0);
                card[x][y].transform.rotation = Quaternion.Euler(r);
                card[x][y].Buttonnum = x;
                card[x][y].InGame = false;
                card[x][y].X = x;
                card[x][y].Y = y;


            }

        }
        MapSizeX = info.MapSizeX;
        MapSizeY = info.MapSizeY;
        cardUse = new CardUseBase[info.MapSizeX + 1][];
        for (int x = 0; x <= info.MapSizeX; x++)
        {
            cardUse[x] = new CardUseBase[info.MapSizeY + 1];
        }

        for (int i = 0; i < info.cardInfos.Count; ++i)
        {

            int x = info.cardInfos[i].X;
            int y = info.cardInfos[i].Y;
            int id = info.cardInfos[i].card_id;

            if (id <= 3)
                cardUse[x][y] = (MagicCard)GameObject.Instantiate(Cards[id]).GetComponent<MagicCard>();
            else
                cardUse[x][y] = (SummonCard)GameObject.Instantiate(Cards[id]).GetComponent<SummonCard>();
            cardUse[x][y].card_id = id;
            float X = x * HexW;
            float Y = y * HexH;
            Vector3 v = new Vector3(X, 0.3f, Y);
            cardUse[x][y].transform.position = v;

            Vector3 r = new Vector3(90, 0, 0);
            cardUse[x][y].transform.rotation = Quaternion.Euler(r);
            cardUse[x][y].Buttonnum = x;
            cardUse[x][y].InGame = false;
            cardUse[x][y].X = x;
            cardUse[x][y].Y = y;
            card[x][y].SetCost(cardUse[x][y].cost);
        }

    }
    public void LoadCard()
    {

        card = new CardBase[MapSizeX + 1][];
        for (int x = 0; x <= MapSizeX; x++)
        {
            card[x] = new CardBase[MapSizeY + 1];
            for (int y = 0; y <= MapSizeY; y++)
            {
                float X = x * HexW;
                float Y = y * HexH;
                Vector3 v = new Vector3(X, 0, Y);
                card[x][y] = ((GameObject)Instantiate(GO_hex)).GetComponent<CardBase>();

                card[x][y].transform.position = v;

                Vector3 r = new Vector3(90, 0, 0);
                card[x][y].transform.rotation = Quaternion.Euler(r);
                card[x][y].Buttonnum = x;
                card[x][y].InGame = false;


            }

        }

        cardUse = new CardUseBase[MapSizeX + 1][];
        for (int x = 0; x <= MapSizeX; x++)
        {
            cardUse[x] = new CardUseBase[MapSizeY + 1];
            for (int y = 0; y <= MapSizeY; y++)
            {

                cardUse[x][y] = (MagicCard)GameObject.Instantiate(Cards[0]).GetComponent<MagicCard>();

                float X = x * HexW;
                float Y = y * HexH;
                Vector3 v = new Vector3(X, 0.3f, Y);
                cardUse[x][y].transform.position = v;

                Vector3 r = new Vector3(90, 0, 0);
                cardUse[x][y].transform.rotation = Quaternion.Euler(r);
                cardUse[x][y].Buttonnum = x;
                cardUse[x][y].InGame = false;
                cardUse[x][y].X = x;
                cardUse[x][y].Y = y;
                card[x][y].SetCost(cardUse[x][y].cost);

            }

        }
        FIleManager.Getinst().SaveCardData();
    }
    void Start()
    {
        //LoadCard();
        CreateXMLmap(FIleManager.Getinst().LoadCardData(path));
    }

    void Update()
    {

    }
}
