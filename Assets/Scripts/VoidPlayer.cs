using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPlayer : MonoBehaviour, Player
{
    int numActions;

    // Start is called before the first frame update
    void Start()
    {
        numActions = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.voidPlayerTurn && numActions > 0)
        {

        }
    }

    private void OnDisable()
    {
    }
}
