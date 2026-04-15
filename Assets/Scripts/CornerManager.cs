using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerManager : MonoBehaviour
{
    [SerializeField] private CornerCollider[] corners;

    public void UpdateCornerSizes(float sizeAddition)
    {
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i].UpdateSize(sizeAddition);
        }
    }
}
