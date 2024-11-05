using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utilities  
{
    public Vector2 GetNewPos(Vector2 range)
    {
        return new Vector2(0 + Random.Range(range.x, range.y), 0 + Random.Range(range.x, range.y));
    }

    public Vector2 GetNewPos(Vector2 range, Vector2 exclusionZone, Vector2 startPos)
    {
        return new Vector2(startPos.x + Random.Range(range.x, range.y), startPos.y + Random.Range(range.x, range.y));
    }

    public List<Vector3> GetPosListAround(Vector3 startPos, float[] ringDistanceArray, int[] posCountArray)
    {       
        List<Vector3> result = new List<Vector3>();
        result.Add(startPos);
        for (int i = 0; i < posCountArray.Length; i++)
        {
            result.AddRange(GetPosListAround(startPos, ringDistanceArray[i], posCountArray[i]));
        }
        return result;
    }

    public List<Vector3> GetPosListAround(Vector3 startPos, float distance, int posCount)
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
