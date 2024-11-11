using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveState { stored, active };

public class DogHolder : MonoBehaviour
{
    private DogBase dog;
    private int index;
    ActiveState activeState;

    public DogBase GetDog() { return dog; }
    public void SetDog(DogBase newDog) { dog = newDog; }
    public int GetIndex() { return index; }
    public void SetIndex(int i) { index = i; }
    public ActiveState GetState() { return activeState; }
    public void SetState(ActiveState state) { activeState = state; }
}
