using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, ISpawnsButtons
{
    UiManager uiManager;

    [SerializeField] private GameObject[] activeSlots = new GameObject[15];
    [SerializeField] private GameObject content;

    private GameObject selectedSlot; 
    private DogPack playerPack;
    private void Start(){
        uiManager = UiManager.instance;
        playerPack = GameObject.FindGameObjectWithTag("Player_Pack").GetComponent<DogPack>();

        selectedSlot = null; 
    }

    public void RefreshInventory(){
        List<DogBase> activeDogs = playerPack.GetActiveDogs();
        List<DogBase> storedDogs = playerPack.GetStoredDogs();

        for(int i = 0; i < activeDogs.Count; i++){
            int index = i; 
            Transform slot = activeSlots[index].transform.GetChild(1);
            slot.GetComponent<Image>().sprite = activeDogs[index].GetSprite();

            Button slotSelected = slot.GetComponent<Button>();
            Button infoButton = activeSlots[index].transform.GetChild(0).GetComponent<Button>();

            infoButton.onClick.RemoveAllListeners();
            slotSelected.onClick.RemoveAllListeners();

            infoButton.onClick.AddListener(() => activeDogs[index].OnButtonClick(activeSlots[index]));
            slotSelected.onClick.AddListener(() => SelectSlot(activeSlots[index]));

            DogHolder dogHolder = activeSlots[index].transform.GetChild(1).GetComponent<DogHolder>();
            dogHolder.SetDog(activeDogs[index]);
            dogHolder.SetIndex(index);
            dogHolder.SetState(ActiveState.active);
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
            dogHolder.SetDog(storedDogs[i]);
            dogHolder.SetIndex(i);
            dogHolder.SetState(ActiveState.stored);
        }
    }

    public void OnButtonClick(GameObject caller){
        SelectSlot(caller);
    }

    private void SelectSlot(GameObject slot){
        if (selectedSlot != null) {  
            SwapSlots(slot);
        }
        else { selectedSlot = slot; print("Selected button"); }
    }

    private void SwapSlots(GameObject slot) {
        DogHolder selectedHolder;
        selectedHolder = selectedSlot.GetComponent<DogHolder>() != null 
            ? selectedSlot.GetComponent<DogHolder>()
            : selectedSlot.transform.GetChild(1).GetComponent<DogHolder>();

        DogHolder slotHolder;
        slotHolder = slot.GetComponent<DogHolder>() != null 
            ? slotHolder = slot.GetComponent<DogHolder>()
            : slot.transform.GetChild(1).GetComponent<DogHolder>();

        if (selectedHolder.GetState() == ActiveState.active && slotHolder.GetState() == ActiveState.active){
            playerPack.SwapDogs(selectedHolder.GetIndex(), slotHolder.GetIndex(), playerPack.GetActiveDogs());
            playerPack.MoveAllDogs(); 
        }
        else if (selectedHolder.GetState() == ActiveState.stored && slotHolder.GetState() == ActiveState.stored){
            playerPack.SwapDogs(selectedHolder.GetIndex(), slotHolder.GetIndex(), playerPack.GetStoredDogs());
            playerPack.MoveAllDogs();
        }
        else if (selectedHolder.GetState() == ActiveState.stored && slotHolder.GetState() == ActiveState.active){
            playerPack.DeactivateDog(slotHolder.GetDog());
            playerPack.ActivateDog(selectedHolder.GetDog()); 
        }
        else{
            playerPack.DeactivateDog(selectedHolder.GetDog());
            playerPack.ActivateDog(slotHolder.GetDog());
        }

        selectedSlot = null; 
        RefreshInventory();
    }
}
 