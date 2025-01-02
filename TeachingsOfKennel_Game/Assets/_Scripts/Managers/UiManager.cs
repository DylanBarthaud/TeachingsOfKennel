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
    [SerializeField] public GameObject packViewer;
    [SerializeField] public GameObject dogStats;
    [SerializeField] public GameObject packButtonContainer;

    [SerializeField] private GameObject[] popUps; 

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
    public GameObject[] SpawnButtons(List<ButtonDataStruct> buttonData, int buttonId, int amount, Transform transform){
        GameObject[] buttonObjs = spawner.SpawnGameObject(buttonList.GetButtons(buttonId), amount, transform);

        int i = 0;
        foreach (GameObject buttonObj in buttonObjs) {
            int index = i; 
            Button button = buttonObj.GetComponent<Button>();
            Image image = buttonObj.GetComponent<Image>();

            if (buttonData[i].eventParent != null){
                button.onClick.AddListener(() => buttonData[index].eventParent.OnButtonClick(buttonObj));
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

        return buttonObjs;
    }

    // Removes all buttons from a given transform
    public void ClearContainer(Transform container){
        foreach (Transform child in container){
            if (child.GetComponent<Button>() != null){
                Destroy(child.gameObject);
            }
        }
    }

    // Activate/Deactivate given window
    public void ActivateWindow(GameObject window){  
        window.SetActive(true);
    }
    public void DeactivateWindow(GameObject window){  
        window.SetActive(false);
    }

    // Handles the dog stat window
    public void SetDogStatWindow(DogDataStruct dogData){
        Transform dogstatTransform = dogStats.transform;
        dogstatTransform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = dogData.icon;
        dogstatTransform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = dogData.description;
        dogstatTransform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Breed: " + dogData.breed;
        dogstatTransform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Faith: " + dogData.faith;
        dogstatTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Bark Strength: " + dogData.barkStrength;
        dogstatTransform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Bark Speed: " + dogData.barkSpeed;
    }

    //PopUps
    public void SpawnDmgPopUp(Vector2 spawnPos, float dmgAmount){
        Transform dmgPopUpTransform = Instantiate(popUps[0], spawnPos, Quaternion.identity).transform;
        DmgPopUp dmgPopUp = dmgPopUpTransform.GetComponent<DmgPopUp>();

        dmgPopUp.SetUp(dmgAmount);
    }
}
