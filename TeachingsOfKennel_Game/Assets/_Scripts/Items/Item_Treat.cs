using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Treat : ItemBase{
    protected override void ActivateItem(){
        Game_Engine.instance.GetPlayerPack().SetFaith(10); 
    }
}
