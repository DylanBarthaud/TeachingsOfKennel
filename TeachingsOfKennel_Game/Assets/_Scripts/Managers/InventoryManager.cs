using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, ISpawnsButtons
{
    UiManager uiManager;

    [SerializeField] private GameObject[] activeSlots = new GameObject[15];
    [SerializeField] private GameObject content;
    [SerializeField] private Transform activeSlotContainer;
    [SerializeField] private Sprite emptySlotSprite; 

    private GameObject selectedSlot; 
    private Player_DogPack playerPack;
    private void Start(){
        uiManager = UiManager.instance;
        playerPack = Game_Engine.instance.GetPlayerPack();

        selectedSlot = null;  
    }

    public void RefreshInventory(){
        List<DogBase> activeDogs = playerPack.GetActiveDogs();
        List<DogBase> storedDogs = playerPack.GetStoredDogs();

        for(int i = 0; i < activeSlots.Length; i++){
            int index = i;
            Transform slot = activeSlots[index].transform.GetChild(0);

            Button slotSelected = slot.GetComponent<Button>();
            Button infoButton = activeSlots[index].transform.GetChild(1).GetComponent<Button>();
            Button storeDogButton = activeSlots[index].transform.GetChild(2).GetComponent<Button>();

            infoButton.onClick.RemoveAllListeners();
            slotSelected.onClick.RemoveAllListeners();
            storeDogButton.onClick.RemoveAllListeners();

            if (index < activeDogs.Count) {
                slot.GetComponent<Image>().sprite = activeDogs[index].GetSprite();

                infoButton.onClick.AddListener(() => activeDogs[index].OnButtonClick(activeSlots[index]));
                slotSelected.onClick.AddListener(() => SelectSlot(activeSlots[index]));
                storeDogButton.onClick.AddListener(() => StoreDog(index));

                DogHolder dogHolder = slot.GetComponent<DogHolder>();
                dogHolder.SetDog(activeDogs[index]);
                dogHolder.SetIndex(index);
                dogHolder.SetState(ActiveState.active);
            }

            else{
                slot.GetComponent<Image>().sprite = emptySlotSprite;
            }
        }

        List<ButtonDataStruct> dogButtons = new List<ButtonDataStruct>();
        for (int i = 0; i < storedDogs.Count; i++){
            ButtonDataStruct newButtonData = new ButtonDataStruct {
                text = null,
                sprite = storedDogs[i].GetSprite(),
                eventParent = this,
                transformParent = null 
            }; 
            dogButtons.Add(newButtonData);
        }
        uiManager.ClearContainer(content.transform);
        GameObject[] buttons = uiManager.SpawnButtons(dogButtons, 2, storedDogs.Count, content.transform);

        for (int i = 0; i < buttons.Length; i++){
            DogHolder dogHolder = buttons[i].GetComponent<DogHolder>(); 
            dogHolder.SetIndex(i);
            dogHolder.SetState(ActiveState.stored);
        }
    }

    public void OnButtonClick(GameObject caller){
        SelectSlot(caller);
    }

    private void SelectSlot(GameObject slot){
        DogHolder slotInfo_1;
        slotInfo_1 = slot.GetComponent<DogHolder>() != null
            ? slotInfo_1 = slot.GetComponent<DogHolder>()
            : slot.transform.GetChild(0).GetComponent<DogHolder>();

        if (slotInfo_1.GetState() == ActiveState.stored) {  
            if(playerPack.GetActiveDogs().Count == 15){
                return; 
            }
            playerPack.ActivateDog(slotInfo_1.GetIndex());
            RefreshInventory();
        }

        else{
            if (selectedSlot != null){
                SwapSlots(slotInfo_1);
            }
            else { selectedSlot = slot; print("Selected button"); }
        }
    }

    private void SwapSlots(DogHolder slotInfo_1) {
        DogHolder slotInfo_2;
        slotInfo_2 = selectedSlot.GetComponent<DogHolder>() != null 
            ? selectedSlot.GetComponent<DogHolder>()
            : selectedSlot.transform.GetChild(0).GetComponent<DogHolder>();

        playerPack.SwapDogs(slotInfo_2.GetIndex(), slotInfo_1.GetIndex(), playerPack.GetActiveDogs());
        playerPack.MoveAllDogs();

        selectedSlot = null; 
        RefreshInventory();
    }

    private void StoreDog(int index) {
        if (index >= 0 && index < playerPack.GetActiveDogs().Count && playerPack.GetActiveDogs().Count > 1){
            playerPack.DeactivateDog(index);
            RefreshInventory();
        } 
    }
}
 