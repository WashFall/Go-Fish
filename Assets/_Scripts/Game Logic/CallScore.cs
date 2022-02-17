using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallScore : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.DeclareWinner();
    }

}
