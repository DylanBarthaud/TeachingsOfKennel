using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DogPack : DogPack{
    private List<ItemBase> items = new List<ItemBase>(); 

    private void Update(){

        if (Input.GetKeyDown(KeyCode.Mouse1)){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveFlag(mousePos);
        }

       SetPos();
    }

    public void addItem(ItemBase item){  
        items.Add(item);
    }

    public void removeItem(ItemBase item){ 
        items.Remove(item); 
    }

    public List<ItemBase> getItems(){  
        return items;
    }
}
