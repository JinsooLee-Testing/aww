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
            nearUserPlayer.GetDamage(35);
            Debug.Log("AIPlayer Attack!!");
        }
        pm.TurnOver();
    }
    public void MoveToNearUserPlayer(PlayerBase aiplayer)
    {
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
            List<Hex> path = mm.GetPath(aiplayer.CurHex, nearUserPlayer.CurHex);

            if (path.Count > aiplayer.MoveRange)
            {
                path.RemoveRange(aiplayer.MoveRange,path.Count-aiplayer.MoveRange);
                
            }
            aiplayer.MoveHexes = path;
            if(nearUserPlayer.CurHex.MapPos==aiplayer.MoveHexes[aiplayer.MoveHexes.Count-1].MapPos)
            {
                aiplayer.MoveHexes.RemoveAt(aiplayer.MoveHexes.Count - 1);
            }
            if (aiplayer.MoveHexes.Count == 0)
            {
                return;
            }
            aiplayer.act = ACT.MOVING;
        }

    }
}
	
 
