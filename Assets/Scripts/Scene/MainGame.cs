using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainGame : BaseScene   // MainGame 클래스는 BaseScene 클래스의 기능을 기반으로 게임 시작 시 필요한 초기화 작업을 수행하는 클래스
{
    public GameObject[] roundStage;

    
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    protected override void Init()
    {
       
        base.Init();
   
    }

    private void Start()
    {
        Managers.Game.GameStart();
        Managers.UI.ShowPopUpUI<StageGuide>();
        Init();
        for(int i =0;i<14;i++) // 맵의 개수만큼 Enemy 개수 설정
        {
            Managers.Game.roundEnemyCount[i] = roundStage[i].transform.childCount; // 하위 오브젝트의 개수를 세는 방식으로 작동
        }
    }

        public void Option()
    {
        Managers.UI.ShowPopUpUI<Option>();
    }
}
