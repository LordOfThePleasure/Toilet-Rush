using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public RunnerType type;

    public bool occupied = false;

    public bool CompareType(RunnerType type)
    {
        return this.type == type || this.type == RunnerType.Both;
    }
}
