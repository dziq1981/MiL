using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controls : PlayerControls
{

    
    void Start() {
        setup();
        jump = "JumpPlayer2";
        horizontal = "HorizontalPlayer2";
        fire1 = "Fire1Player2";
        otherPlayer = PlayerManager.me.Player1;
    }

    public override GameObject getOtherPlayer()
    {
        return PlayerManager.me.Player1;
    }

}
