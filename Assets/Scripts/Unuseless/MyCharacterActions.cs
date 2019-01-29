using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MyCharacterActions : PlayerActionSet
{
    public PlayerAction Attack;
    public PlayerAction Skiil01;
    public PlayerAction Skiil02;
    public PlayerAction Skiil03;
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerOneAxisAction Move;



    public MyCharacterActions()
    {
        Attack = CreatePlayerAction("Attack");
        Skiil01 = CreatePlayerAction("Skiil01");
        Skiil02 = CreatePlayerAction("Skiil02");
        Skiil03 = CreatePlayerAction("Skiil03");
        Move = CreateOneAxisPlayerAction(Left, Right);
    }
}

