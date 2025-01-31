using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TipsData", menuName = "Loading/TipsData")]
public class TipsDataSO : ScriptableObject
{
    public List<string> tips;
}
