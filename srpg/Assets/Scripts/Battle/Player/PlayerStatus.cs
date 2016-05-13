using UnityEngine;
using System.Collections;

public class PlayerStatus
{
    public string Name = "wolf";
    public int MoveRange = 4;
    public float MoveSpeed = 5.0f;
    public int attackRange = 1;
    public float Maxhp = 150;
    public float Curhp = 150;
    public int Attack = 30;
    public string info;


    public PlayerStatus()
    {
        Name = "wolf";
        MoveRange = 4;
        MoveSpeed = 5.0f;
        attackRange = 1;
        Curhp = 150;
        Maxhp = Curhp;
        Attack = 80;
    }
}
