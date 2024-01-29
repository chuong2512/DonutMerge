using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxDestroy : MonoBehaviour
{
    private void Start()
    {
        DestroyObjectDelayed();
    }

    void DestroyObjectDelayed()
    {
        Destroy(gameObject, 1);
    }
}