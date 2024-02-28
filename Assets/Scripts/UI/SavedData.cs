using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeveSaved", menuName = "SavedData")]
public class SavedData : ScriptableObject
{
    public bool[] unlockedLevels;
}
