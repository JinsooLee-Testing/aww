using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIthink  {
    private static AIthink inst = null;
    public static AIthink GetInst()
    {
        if (inst == null)
        {
            inst = new AIthink();
        }
        return inst;
    }
    // Use this for initialization
    public void AtkAItoUser(PlayerBase aiplayer)
    {
        //근접한 유저플레이어를 찾는다. -> 찾았따면 공격을 한다 -> 못찾았다면 턴을 넘긴다
        PlayerManager pm = PlayerManager.GetInst();
        MapManager mm = MapManager.GetInst();
        PlayerBase nearUserPlayer = null;
        int nearDistance = 1000;
        //근접 플레이어 서치
        foreach (PlayerBase up in pm.Players)
        {
            if (up is UserPlayer)
            {
                int distance = mm.GetDistance(up.CurHex, aiplayer.CurHex);
                if (nearDistance > distance)
                {
                    nearUserPlayer = up;
                    nearDistance = distance;
                }
            }
        }
        if(nearUserPlayer!=null)
        {

            aiplayer.anim.SetBool("attack", true);
            Vector3 v = aiplayer.transform.position;
            v.y = PlayerManager.GetInst().m_y;
            Vector3 v2 = nearUserPlayer.CurHex.transform.position;
            v2.y = PlayerManager.GetInst().m_y;
            aiplayer.transform.rotation = Quaternion.LookRotation((v2 - v).normalized);
            if (aiplayer.Monster_id != 1)
            {
                Vector3 r = aiplayer.transform.rotation.eulerAngles;
                r.y -= 90;
                aiplayer.transform.rotation = Quaternion.Euler(r);
            }
            nearUserPlayer.GetDamage(aiplayer.status.Attack);

            
            EffectManager.GetInst().ShowEffect(nearUserPlayer.gameObject);
            SoundManager.GetInst().PlayAttackSound();
           
            aiplayer.act = ACT.ATTACKING;
            aiplayer.CurHex.Passable = false;
      
            
            // BattleManager.GetInst().AttackAtoB(aiplayer, nearUserPlayer);
            Debug.Log("AIPlayer Attack!!");
        }
      //  aiplayer.act = ACT.IDLE;
        pm.TurnOver();
    }
    public void MoveToNearUserPlayer(PlayerBase aiplayer)
    {
        PlayerManager pm = PlayerManager.GetInst();
        MapManager mm = MapManager.GetInst();
        PlayerBase nearUserPlayer = null;
        int nearDistance = 50;
        //근접 플레이어 서치
        int i = Random.Range(1, 5);
       
            foreach (PlayerBase up in pm.Players)
            {
                if (up is UserPlayer)
                {
                    int distance = mm.GetDistance(up.CurHex, aiplayer.CurHex);
                    if (nearDistance > distance)
                    {
                        nearUserPlayer = up;
                        nearDistance = distance;
                    }
                }
            }
 
        if (aiplayer.m_type == Type.MONSTER|| CostManager.GetInst().enemy_cost_num < 3)
        {
            if (nearUserPlayer != null)
            {

                List<Hex> path = mm.GetPath(aiplayer.CurHex, nearUserPlayer.CurHex);

                if (path == null)
                    PlayerManager.GetInst().TurnOver();
                else
                {
                    if (path.Count > aiplayer.status.MoveRange)
                    {
                        path.RemoveRange(aiplayer.status.MoveRange, path.Count - aiplayer.status.MoveRange);

                    }
                    aiplayer.MoveHexes = path;
                    if (nearUserPlayer.CurHex.MapPos == aiplayer.MoveHexes[aiplayer.MoveHexes.Count - 1].MapPos)
                    {
                        aiplayer.MoveHexes.RemoveAt(aiplayer.MoveHexes.Count - 1);
                    }
                    if (aiplayer.MoveHexes.Count ==0)
                    {
                       
                        AtkAItoUser(aiplayer);
                        return;
                    }
                    aiplayer.act = ACT.MOVING;
                }
                MapManager.GetInst().ResetMapColor(aiplayer.CurHex.MapPos);
            }
        }
        else if (CostManager.GetInst().enemy_cost_num >= 3 && CostManager.GetInst().enemy_cost_num<5 && aiplayer.m_type == Type.BOSS)
        {
            List<Hex> path = mm.GetPath(aiplayer.CurHex, nearUserPlayer.CurHex);

            if (path == null)
                PlayerManager.GetInst().TurnOver();
            Point v = aiplayer.CurHex.MapPos;
            int su_x = 0;
            int su_y = 0;
            for (int j = 0; j < MapManager.GetInst().MapSizeX; ++j)
            {
                for (int k = 0; k < MapManager.GetInst().MapSizeZ; ++k)
                {
                    int x = Random.Range(-3, 3);
                    int z = Random.Range(-3, 3);

                    x = v.GetX() + x;
                    z = v.GetZ() + z;

                    if ((int)x > MapManager.GetInst().MapSizeX)
                        x = MapManager.GetInst().MapSizeX-1;
                    if ((int)z > MapManager.GetInst().MapSizeZ)
                        z = MapManager.GetInst().MapSizeZ-1;
                    if ((int)x <= 0)
                        x = 1;
                    if ((int)z <= 0)
                       z = 1;
                
                    if (MapManager.GetInst().Map[x][0][z].Passable == true)
                    {
                        su_x = x;
                        su_y = z;
                        break;
                    }
                }
            }
            PlayerManager.GetInst().GenAIPlayer(su_x, su_y);
            EffectManager.GetInst().ShowEffect_Summon(aiplayer.CurHex.gameObject, 6, 0f);
            CostManager.GetInst().enemy_cost_num -= 3;
            //PlayerManager.GetInst().TurnOver();
        }
        else if (CostManager.GetInst().enemy_cost_num<8 && aiplayer.m_type == Type.BOSS)
        {
            aiplayer.act = ACT.CASTING;
            EffectManager.GetInst().ShowEffect_Summon(aiplayer.CurHex.gameObject, 9, 0.0f);
            CostManager.GetInst().enemy_cost_num -= 5;
            PlayerManager.GetInst().TurnOver();
        }
        else
        {
            if (aiplayer.m_type == Type.BOSS)
            {
                List<Hex> path = mm.GetPath(aiplayer.CurHex, nearUserPlayer.CurHex);

                if (path == null)
                    PlayerManager.GetInst().TurnOver();
                Vector3 v = aiplayer.transform.position;
     
          
                if (path.Count < 5)
                {
                    if (CostManager.GetInst().enemy_cost_num > 5)
                    {

                        aiplayer.act = ACT.CASTING;
                        EffectManager.GetInst().ShowEffect_Summon(aiplayer.CurHex.gameObject, 9, 0.0f);
                        CostManager.GetInst().enemy_cost_num -= 5;
                        PlayerManager.GetInst().TurnOver();
                    }
                }
                else
                {
                    if (nearUserPlayer != null)
                    {

                        if (path == null)
                            PlayerManager.GetInst().TurnOver();
                        else
                        {
                            if (path.Count > aiplayer.status.MoveRange)
                            {
                                path.RemoveRange(aiplayer.status.MoveRange, path.Count - aiplayer.status.MoveRange);

                            }
                            aiplayer.MoveHexes = path;
                            if (nearUserPlayer.CurHex.MapPos == aiplayer.MoveHexes[aiplayer.MoveHexes.Count - 1].MapPos)
                            {
                                aiplayer.MoveHexes.RemoveAt(aiplayer.MoveHexes.Count - 1);
                            }

                            if (aiplayer.MoveHexes.Count == 0)
                            {
                                AtkAItoUser(aiplayer);

                                return;
                            }
                            aiplayer.act = ACT.MOVING;

                        }
                        MapManager.GetInst().ResetMapColor(aiplayer.CurHex.MapPos);
                    }
                }

                Point p = aiplayer.CurHex.MapPos;
                int su_x = 0;
                int su_y = 0;
                for (i = 0; i < 2; ++i)
                {
                    for (int j = 0; j < MapManager.GetInst().MapSizeX; ++j)
                    {
                        for (int k = 0; k < MapManager.GetInst().MapSizeZ; ++k)
                        {
                            int x = Random.Range(-3, 3);
                            int z = Random.Range(-3, 3);

                            x = p.GetX() + x;
                            z = p.GetZ() + z;

                            if ((int)x > MapManager.GetInst().MapSizeX)
                                x = MapManager.GetInst().MapSizeX - 1;
                            if ((int)z > MapManager.GetInst().MapSizeZ)
                                z = MapManager.GetInst().MapSizeZ - 1;
                            if ((int)x <= 0)
                                x = 1;
                            if ((int)z <= 0)
                                z = 1;

                            if (MapManager.GetInst().Map[x][0][z].Passable == true)
                            {
                                su_x = x;
                                su_y = z;
                                break;
                            }
                        }
                    }
                    PlayerManager.GetInst().GenAIPlayer(su_x, su_y);
                    EffectManager.GetInst().ShowEffect_Summon(aiplayer.CurHex.gameObject, 6, 0f);
                    CostManager.GetInst().enemy_cost_num -= 3;

                }

            }
        }
       


    }
}
	
 
