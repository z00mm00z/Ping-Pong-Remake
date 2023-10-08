using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{

    [SerializeField] GameObject BotStats;

    private void Start() {
        if(!Debug.isDebugBuild) {
            Destroy(BotStats.gameObject);
        }
    }
}
