using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    // Spawns and updates UI elements 
    // Holds lists of UI presets 

    [SerializeField] SpawnGameObjects spawner;
    [SerializeField] ButtonList buttonList;

    [SerializeField] public GameObject statContainer; 

    public static UiManager instance;
    private void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else { GameObject.Destroy(gameObject); }
    }

    // Spawns amount of buttons in location  
    // Sets all button data including name, text and OnClick event 
    // All Based on given inputs
    public void SpawnButtons(List<ButtonDataStruct> buttonData, int buttonId, int amount, Transform transform){
        GameObject[] buttonObjs = spawner.SpawnGameObject(buttonList.GetButtons(buttonId), amount, transform);

        int i = 0;
        foreach (GameObject buttonObj in buttonObjs) {
            Button button = buttonObj.GetComponent<Button>();
            Image image = buttonObj.GetComponent<Image>();

            if (buttonData[i].eventParent != null){
                button.onClick.AddListener(buttonData[i].eventParent.OnButtonClick);
            }
            else { Debug.LogWarning(buttonObj + i.ToString() + ": hasn't been set an event parent"); }

            if (buttonData[i].text != null){
                TextMeshProUGUI text = buttonObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                text.text = buttonData[i].text;
            }

            if (buttonData[i].sprite != null){
                image.sprite = buttonData[i].sprite;
            }

            if (buttonData[i].transformParent != null){
                buttonObj.GetComponent<MoveButtonToTarget>().SetTarget(buttonData[i].transformParent); 
            }

            i++;
        }
    }

    // Removes all buttons from a given transform
    public void RemoveButtons(Transform container){
        foreach (Transform child in container){
            if (child.GetComponent<Button>() != null){
                Destroy(child.gameObject);
            }
        }
    }
}
