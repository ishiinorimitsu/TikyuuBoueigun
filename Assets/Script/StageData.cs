using UnityEngine;

[System.Serializable]
public class StageData 
{
    public int stageNo;    //ステージの番号例えば「1,始まりの町」の1の部分

    public string subTitle;    //ステージの「1,始まりの町」の「始まりの町」の部分

    //------------------------------どこにだれを何体作るか----------------------------------------------//
    [Header("Wave1")]
    public int wave1GenerateEnemyTranIndex;     //生成地点はEnemyGeneratorに配列として入っているから、その中の何番の地点に生成するかのインデックス番号を入れる。

    public int wave1DinosaurCount;     //恐竜を何体作るか

    public int wave1InsectCount;     //ロボットを何体作るか

    public int wave1BossDinosaur;   //ボス恐竜を何体作るか

    public int wave1BossInsect;   //ボスロボットを何体作るか

    [Header("Wave2")]
    public int wave2GenerateEnemyTranIndex;     //生成地点はEnemyGeneratorに配列として入っているから、その中の何番の地点に生成するかのインデックス番号を入れる。

    public int wave2DinosaurCount;     //恐竜を何体作るか

    public int wave2InsectCount;     //ロボットを何体作るか

    public int wave2BossDinosaur;   //ボス恐竜を何体作るか

    public int wave2BossInsect;   //ボスロボットを何体作るか

    [Header("Wave3")]
    public int wave3GenerateEnemyTranIndex;     //生成地点はEnemyGeneratorに配列として入っているから、その中の何番の地点に生成するかのインデックス番号を入れる。

    public int wave3DinosaurCount;     //恐竜を何体作るか

    public int wave3InsectCount;     //ロボットを何体作るか

    public int wave3BossDinosaur;   //ボス恐竜を何体作るか

    public int wave3BossInsect;   //ボスロボットを何体作るか

    [Header("合計")]
    public int totalDinosaurCount;     //UIに表示するように合計も数えておく

    public int totalInsectCount;      //UIに表示するように作っておく

    public int totalBossCount;    //UIに表示するように作っておく

    //public int GenerateEnemyTranIndex;     //生成地点はEnemyGeneratorに配列として入っているから、その中の何番の地点に生成するかのインデックス番号を入れる。

    //public int DinosaurCount;     //恐竜を何体作るか

    //public int InsectCount;     //虫を何体作るか
}
