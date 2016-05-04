using UnityEngine;
using System.Collections;


public class BattleCardManager : MonoBehaviour
{
    public bool active = true;
    private static BattleCardManager inst = null;
    public GameObject GO_hex;
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
    }
    public void useCard(int but_num)
    {
        card[but_num].On_active = false;
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
    public void RandomDrawCard()
    {


        Vector3 temp = Initpos2;
        for (int x = 0; x <= MapSizeX; x++)
        {
            int i = Random.Range(0, 4);
            GameObject.Destroy(cardUse[x].gameObject);
            if (i == 0)
            {

                cardUse[x] = ((GameObject)Instantiate(GO_SUMMON)).GetComponent<SummonCard>();
            }
            else if (i == 1)
            {
                cardUse[x] = ((GameObject)Instantiate(GO_wall)).GetComponent<MagicCard>();
            }
            else if (i == 2)
            {
                cardUse[x] = ((GameObject)Instantiate(GO_water)).GetComponent<MagicCard>();
            }
            else if (i >= 3)
            {
                cardUse[x] = ((GameObject)Instantiate(GO_Fire)).GetComponent<MagicCard>();
            }
            cardUse[x].transform.position = temp;
            cardUse[x].Buttonnum = x;
            cardUse[x].On_active = true;
            card[x].SetCost(cardUse[x].cost);
            temp.x += 5;
        }
    
    }
    public void RandomDraw()
    {
        Vector3 temp = Initpos2;
        for (int x = 0; x <= MapSizeX; x++)
        {
            int i = Random.Range(0, 4);
            if(i==0)
                cardUse[x] = ((GameObject)Instantiate(GO_SUMMON)).GetComponent<SummonCard>();
            else if(i==1)
                cardUse[x] = ((GameObject)Instantiate(GO_wall)).GetComponent<MagicCard>();
            else if (i == 2)
                cardUse[x] = ((GameObject)Instantiate(GO_water)).GetComponent<MagicCard>();
            else if (i >= 3)
                cardUse[x] = ((GameObject)Instantiate(GO_Fire)).GetComponent<MagicCard>();
            cardUse[x].transform.position = temp;
            cardUse[x].Buttonnum = x;
            card[x].On_active = true;
            card[x].SetCost(cardUse[x].cost);
            temp.x += 5;
        }
    }
    public void LoadCard()
    {

        card = new CardBase[MapSizeX*2 + 1];
        for (int x = 0; x <= MapSizeX; x++)
        {

            card[x] = ((GameObject)Instantiate(GO_hex)).GetComponent<CardBase>();
            card[x].transform.position = Initpos;
            
            card[x].Buttonnum = x;
            Initpos.x += 5;
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
