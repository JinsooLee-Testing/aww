using UnityEngine;
using System.Collections;

public class CostManager : MonoBehaviour
{
    private static CostManager inst = null;
    public GameObject GO_hex;
    public  Vector3 Initpos;
    public int MapSizeX;
    public int MapSizeY;

    int cur_cost_max=1;
    public int cur_cost_num=1;
    costBase[] cost;
    public static CostManager GetInst()
    {
        return inst;
    }
    public void AddCost()
    {
        cur_cost_max++;
        cur_cost_num= cur_cost_max;
        SetCost();
        BattleCardManager.GetInst().RandomDrawCard();
    }
    public void CostDecrease(int num)
    {
        cur_cost_num-= num;
        
        for (int i = 0; i < cur_cost_max; ++i)
        {
            if (cur_cost_num <= i)
                cost[MapSizeX - i].SetEmpty(true);
        }
       
    }
   void SetCost()
    {
        for (int i = 0; i < cur_cost_max; ++i)
        {
            if(cur_cost_max>i)
                cost[MapSizeX-i].SetEmpty(false);
        }
    }
    void SetEmpty()
    {
        for (int i = cur_cost_max; i > 0; --i)
        {
          //  cost[i].SetEmpty(true);
        }
    }
    public void DrawCost()
    {
        cost = new costBase[MapSizeX + 1];
        for (int x = 0; x <= MapSizeX; x++)
        {

            cost[x] = ((GameObject)Instantiate(GO_hex)).GetComponent<costBase>();
            cost[x].transform.position = Initpos;
            Initpos.x += 3;
        }
    }
    void Awake()
    {
        inst = this;
    }
    void Start()
    {
        
        DrawCost();
        SetCost();
    }
    void Update()
    {
    }
 
}
