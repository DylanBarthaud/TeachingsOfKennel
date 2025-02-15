using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, ISpawnsButtons
{
    [SerializeField] protected string itemName; 
    [SerializeField] protected Sprite sprite;

    [SerializeField] protected bool isExhaustible;
    [SerializeField] protected int charges;
    [SerializeField] protected float coolDownTimer;

    [SerializeField]  private float currentTimer;
    private bool isOnCD;
    private ButtonDataStruct buttonData;

    private void Start(){
        buttonData = new ButtonDataStruct(){
            eventParent = this,
            text = itemName,
            sprite = this.sprite,
        };

        currentTimer = coolDownTimer;
    }

    private void Update(){
        if (!isOnCD) {
            return;
        }

        if (currentTimer <= 0) {  
            isOnCD = false;
            currentTimer = coolDownTimer;
        }

        currentTimer -= Time.deltaTime;
    }

    public void OnButtonClick(GameObject buttonObj){
        if (isOnCD){
            print("On CD");
            return;
        }

        ActivateItem();
        charges--;

        if (isExhaustible && charges <= 0){
            Game_Engine.instance.GetPlayerPack().removeItem(this);
            Destroy(gameObject);
        }

        UiManager.instance.SetUpItemWindow();
        isOnCD = true; 
    }

    protected abstract void ActivateItem(); 

    public ButtonDataStruct GetButtonData(){
        return buttonData;
    }

    public string GetItemName(){
        return itemName;
    }

    public void AddCharges(int charges){  
        this.charges += charges;
    }

    protected void OnTriggerEnter2D(Collider2D collision){
        var player = collision.gameObject.GetComponent<Player_DogPack>(); 

        if (player != null) {
            List<ItemBase> list = player.getItems();
            foreach (ItemBase item in list) {  
                if (item.GetItemName() == GetItemName()){
                    item.AddCharges(charges);
                    Destroy(gameObject);
                    return; 
                }
            }

            player.addItem(this);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.position = new Vector3(10, 0, 0);
            transform.parent = GameObject.Find("ActiveItems").transform; 
        }
    }
}
