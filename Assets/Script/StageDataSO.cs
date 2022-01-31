using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "Create StageDataSO")]
public class StageDataSO : ScriptableObject
{
    public List<StageData> StageDataList = new List<StageData>();

    //public List<StageData> totalStageDataList = new List<StageData>();
}
