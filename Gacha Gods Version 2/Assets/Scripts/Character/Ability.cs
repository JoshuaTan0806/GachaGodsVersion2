using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    CharacterStats stats;

    public void Initialise(CharacterStats stats)
    {
        this.stats = stats;
    }
}
