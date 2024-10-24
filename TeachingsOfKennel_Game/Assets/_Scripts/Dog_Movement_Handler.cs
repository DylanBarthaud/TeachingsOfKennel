using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Movement_Handler : MonoBehaviour
{

    public void MoveAllDogGraphics(List<DogBase> dogs, Vector3 vector, float speed)
    {
        List<Vector3> targetPositions = GetPosListAround(vector, new float[] {0.25f, 0.5f, 0.75f, 1f}, new int[] { 5, 10, 15 , dogs.Count - 31 });

        int targetPositionIndex = 0;
        foreach (DogBase dogBase in dogs)
        {
            dogBase.MoveDogGraphic(targetPositions[targetPositionIndex]);
            targetPositionIndex++;
        }
    }

    private List<Vector3> GetPosListAround(Vector3 startPos, float[] ringDistanceArray, int[] posCountArray)
    {
        List<Vector3> result = new List<Vector3>();
        result.Add(startPos);
        for (int i = 0; i < posCountArray.Length; i++)
        {
            result.AddRange(GetPosListAround(startPos, ringDistanceArray[i], posCountArray[i]));
        }
        return result;
    }

    private List<Vector3> GetPosListAround(Vector3 startPos, float distance, int posCount)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < posCount; i++)
        {
            float angle = i * (360f / posCount);
            Vector3 dirc = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 pos = startPos + dirc * distance;
            result.Add(pos);
        }
        return result;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
}
