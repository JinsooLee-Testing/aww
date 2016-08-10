using UnityEngine;
using System.Collections;


public class BattleCardManager : MonoBehaviour
{
    public bool active = true;
    private static BattleCardManager inst = null;
    public GameObject[] cards = new GameObject[10]; 
    public GameObject GO_SUMMON;
    public GameObject GO_Fire;
    public GameObject GO_water;
    public GameObject GO_wall;
    public int MapSizeX;
    public int MapSizeY;

    public float HexW; //Awake에서 설정
    public float HexH; //Awake
    public Vector3 Initpos;
    public Vector3 Initpos2;
    public Vector3 Initpos3;
    CardBase[] card;
    public CardUseBase[] cardUse;
    // Use this for initialization

    public static BattleCardManager GetInst()
    {
        return inst;
    }
    void Awake()
    {
        inst = this;
        Initpos3 = Initpos2;
        SetCardSize();
        inst.cards[0] = ((GameObject)Resources.Load("Prefabs/card/cardbase"));
        inst.cards[1] = ((GameObject)Resources.Load("Prefabs/card/summon2"));
        inst.cards[2] = ((GameObject)Resources.Load("Prefabs/card/fire"));
        inst.cards[3] = ((GameObject)Resources.Load("Prefabs/card/water"));
        inst.cards[4] = ((GameObject)Resources.Load("Prefabs/card/wall"));
        inst.cards[5] = ((GameObject)Resources.Load("Prefabs/card/wind"));
        inst.cards[6] = ((GameObject)Resources.Load("Prefabs/card/fireball"));
        inst.cards[7] = ((GameObject)Resources.Load("Prefabs/card/heal"));
        inst.cards[8] = ((GameObject)Resources.Load("Prefabs/card/waterfall"));
        inst.cards[9] = ((GameObject)Resources.Load("Prefabs/card/shield"));
        // inst.cards[4] = ((GameObject)Resources.Load("Prefabs/card/wind"));
    }
    public void useCard(int but_num)
    {
        card[but_num].On_active = false;
    }
    void SetCardSize()
    {
   
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
    public void RandomDrawCard()
    {


        Vector3 temp = Initpos2;
        for (int x = 0; x <= MapSizeX; x++)
        {
            int i = Random.Range(0,9);
            GameObject.Destroy(cardUse[x].gameObject);
            if (i == 0)
            {

                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[2]).GetComponent<MagicCard>();
            }
            else if (i == 1)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[3]).GetComponent<MagicCard>();
            }
            else if (i == 2)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[4]).GetComponent<MagicCard>();
            }
            else if (i == 3)
            {
                cardUse[x] = (SummonCard)GameObject.Instantiate(cards[1]).GetComponent<SummonCard>();
            }
            else if (i == 4)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[5]).GetComponent<MagicCard>();
            }
            else if (i == 5)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[6]).GetComponent<MagicCard>();
            }
            else if (i == 6)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[7]).GetComponent<MagicCard>();
            }
            else if (i==7)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[8]).GetComponent<MagicCard>();
            }
             else
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[9]).GetComponent<MagicCard>();
            }
            cardUse[x].transform.position = temp;
            cardUse[x].Buttonnum = x;
            cardUse[x].On_active = true;
            card[x].SetCost(cardUse[x].cost);
            temp.x += 3;
        }
    
    }
    public void RandomDraw()
    {
        Vector3 temp = Initpos2;
        for (int x = 0; x <= MapSizeX; x++)
        {
            int i = Random.Range(0, 9);
            if (i == 0)
            {

                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[2]).GetComponent<MagicCard>();
            }
            else if (i == 1)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[3]).GetComponent<MagicCard>();
            }
            else if (i == 2)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[4]).GetComponent<MagicCard>();
            }
            else if (i == 3)
            {
                cardUse[x] = (SummonCard)GameObject.Instantiate(cards[1]).GetComponent<SummonCard>();
            }
            else if (i == 4)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[5]).GetComponent<MagicCard>();
            }
            else if (i == 5)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[6]).GetComponent<MagicCard>();
            }
            else if (i == 6)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[7]).GetComponent<MagicCard>();
            }
            else if (i == 7)
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[8]).GetComponent<MagicCard>();
            }
            else
            {
                cardUse[x] = (MagicCard)GameObject.Instantiate(cards[9]).GetComponent<MagicCard>();
            }
            cardUse[x].transform.position = temp;
            cardUse[x].Buttonnum = x;
            card[x].On_active = true;
            card[x].SetCost(cardUse[x].cost);
            temp.x += 3;
        }
    }
    public void LoadCard()
    {

        card = new CardBase[MapSizeX*2 + 1];
        for (int x = 0; x <= MapSizeX; x++)
        {

            card[x] = (CardBase)((GameObject)Instantiate(inst.cards[0])).GetComponent<CardBase>();

            card[x].transform.position = Initpos;
            
            card[x].Buttonnum = x;
            Initpos.x += 3;
            card[x].On_active = true;
 
        }

        cardUse = new CardUseBase[MapSizeX + 1];
        RandomDraw();
    }
    void Start()
    {
        if(active==true)
        LoadCard();

    }

    void Update()
    {
   

    }
}
