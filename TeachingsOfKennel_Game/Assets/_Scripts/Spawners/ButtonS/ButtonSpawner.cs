using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ButtonSpawner : MonoBehaviour
{
    [SerializeField] private ButtonList buttonList;

    public void SpawnButton(ISpawnsButtons caller, int amount, int buttonId, Transform container)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject buttonObj = Instantiate(buttonList.GetButtons(buttonId), container);
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(caller.OnButtonClick);
        }
    }

    public void SpawnButton(ISpawnsButtons caller, int amount, int buttonId, Transform container, GameObject targetTransform)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject buttonObj = Instantiate(buttonList.GetButtons(buttonId), container);
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(caller.OnButtonClick);
            //buttonObj.GetComponent<MoveButtonToTarget>().SetTarget(targetTransform);
        }
    }

    public void SpawnButton(List<ISpawnsButtons> caller, int amount, int buttonId, Transform container){
        for (int i = 0; i < amount; i++){
            GameObject buttonObj = Instantiate(buttonList.GetButtons(buttonId), container);
            Button button = buttonObj.GetComponent<Button>();
            
            button.onClick.AddListener(caller[i].OnButtonClick);
        }
    }

    public void SpawnButton(List<ISpawnsButtons> caller, int amount, int[] buttonIdList, Transform container){
        for (int i = 0; i < amount; i++){
            Instantiate(buttonList.GetButtons(buttonIdList[i]), container);
        }
    }

    public void RemoveButtons(Transform container)
    {
        foreach(Transform child in container){
            if (child.GetComponent<Button>() != null){  
                Destroy(child.gameObject);
            }
        }
    }
}
