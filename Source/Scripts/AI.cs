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
           
            aiplayer.act = ACT.MOVING;
        }

    }
}
	
 
