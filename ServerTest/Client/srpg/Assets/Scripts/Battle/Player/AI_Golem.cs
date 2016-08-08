using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AI_Golem
{
    private static AI_Golem inst = null;
    public static AI_Golem GetInst()
    {
        if (inst == null)
        {
            inst = new AI_Golem();
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
        if (nearUserPlayer != null)
        {

            aiplayer.anim.SetBool("attack", true);
            Vector3 v = aiplayer.transform.position;
            v.y = PlayerManager.GetInst().m_y;
            Vector3 v2 = nearUserPlayer.CurHex.transform.position;
            v2.y = PlayerManager.GetInst().m_y;
            aiplayer.transform.rotation = Quaternion.LookRotation((v2 - v).normalized);

            if (((UserPlayer)nearUserPlayer).equip_type != "shield")
                nearUserPlayer.GetDamage(aiplayer.status.Attack);
            else
                ((UserPlayer)nearUserPlayer).DestroyEquip();


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
        int i = Random.Range(0, 6);

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
  
        if (i < 2)
        {
            if (nearUserPlayer != null)
            {
                Hex temp = nearUserPlayer.CurHex;

                List<Hex> path = mm.GetPath(aiplayer.CurHex, temp);
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
                    if (aiplayer.MoveHexes.Count <= 1)
                    {

                        AtkAItoUser(aiplayer);
                        return;
                    }
                    aiplayer.act = ACT.MOVING;
                }
                MapManager.GetInst().ResetMapColor(aiplayer.CurHex.MapPos);
            }
        }
        else
        {

                aiplayer.act = ACT.CASTING;
                PlayerManager.GetInst().TurnOver();
                EffectManager.GetInst().ShowEffect_Summon(aiplayer.CurHex.gameObject, 9, 0.0f);

        }

    }
}

