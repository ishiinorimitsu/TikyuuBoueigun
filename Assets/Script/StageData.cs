using UnityEngine;

[System.Serializable]
public class StageData 
{
    public int stageNo;    //�X�e�[�W�̔ԍ��Ⴆ�΁u1,�n�܂�̒��v��1�̕���

    public string subTitle;    //�X�e�[�W�́u1,�n�܂�̒��v�́u�n�܂�̒��v�̕���

    //------------------------------�ǂ��ɂ�������̍�邩----------------------------------------------//
    [Header("Wave1")]
    public int wave1GenerateEnemyTranIndex;     //�����n�_��EnemyGenerator�ɔz��Ƃ��ē����Ă��邩��A���̒��̉��Ԃ̒n�_�ɐ������邩�̃C���f�b�N�X�ԍ�������B

    public int wave1DinosaurCount;     //���������̍�邩

    public int wave1InsectCount;     //���{�b�g�����̍�邩

    public int wave1BossDinosaur;   //�{�X���������̍�邩

    public int wave1BossInsect;   //�{�X���{�b�g�����̍�邩

    [Header("Wave2")]
    public int wave2GenerateEnemyTranIndex;     //�����n�_��EnemyGenerator�ɔz��Ƃ��ē����Ă��邩��A���̒��̉��Ԃ̒n�_�ɐ������邩�̃C���f�b�N�X�ԍ�������B

    public int wave2DinosaurCount;     //���������̍�邩

    public int wave2InsectCount;     //���{�b�g�����̍�邩

    public int wave2BossDinosaur;   //�{�X���������̍�邩

    public int wave2BossInsect;   //�{�X���{�b�g�����̍�邩

    [Header("Wave3")]
    public int wave3GenerateEnemyTranIndex;     //�����n�_��EnemyGenerator�ɔz��Ƃ��ē����Ă��邩��A���̒��̉��Ԃ̒n�_�ɐ������邩�̃C���f�b�N�X�ԍ�������B

    public int wave3DinosaurCount;     //���������̍�邩

    public int wave3InsectCount;     //���{�b�g�����̍�邩

    public int wave3BossDinosaur;   //�{�X���������̍�邩

    public int wave3BossInsect;   //�{�X���{�b�g�����̍�邩

    [Header("���v")]
    public int totalDinosaurCount;     //UI�ɕ\������悤�ɍ��v�������Ă���

    public int totalInsectCount;      //UI�ɕ\������悤�ɍ���Ă���

    public int totalBossCount;    //UI�ɕ\������悤�ɍ���Ă���

    //public int GenerateEnemyTranIndex;     //�����n�_��EnemyGenerator�ɔz��Ƃ��ē����Ă��邩��A���̒��̉��Ԃ̒n�_�ɐ������邩�̃C���f�b�N�X�ԍ�������B

    //public int DinosaurCount;     //���������̍�邩

    //public int InsectCount;     //�������̍�邩
}
