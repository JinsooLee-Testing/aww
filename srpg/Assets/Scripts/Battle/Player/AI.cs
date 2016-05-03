﻿using UnityEngine;
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
            aiplayer.anim.SetBool("attack", true);
            aiplayer.act = ACT.ATTACKING;
            aiplayer.CurHex.Passable = false;
            
             //BattleManager.GetInst().AttackAtoB(aiplayer, nearUserPlayer);
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
 
        if (i < 2 || aiplayer.m_type == Type.MONSTER)
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
        else if (i == 4)
        {
            List<Hex> path = mm.GetPath(aiplayer.CurHex, nearUserPlayer.CurHex);

            if (path == null)
                PlayerManager.GetInst().TurnOver();
            Vector3 v = aiplayer.transform.position;
            int x = Random.Range(-5, 5);
            int z = Random.Range(-5, 5);
            if ((int)v.x + x > MapManager.GetInst().MapSizeX)
                v.x = MapManager.GetInst().MapSizeX;
            if ((int)v.z + z > MapManager.GetInst().MapSizeZ)
                v.z = MapManager.GetInst().MapSizeZ;
            if ((int)v.x + x <= 0)
                v.x = 0;
            if ((int)v.z + z <= 0)
                v.z = 0;
            PlayerManager.GetInst().GenAIPlayer((int)v.x, (int)v.z);
            EffectManager.GetInst().ShowEffect_Summon(aiplayer.CurHex.gameObject, 6, 0f);
            PlayerManager.GetInst().TurnOver();
        }
        else
        {
            aiplayer.act = ACT.CASTING;
            EffectManager.GetInst().ShowEffect_Summon(aiplayer.CurHex.gameObject, 9, 0.0f);
            PlayerManager.GetInst().TurnOver();
        }


    }
}
	
 
