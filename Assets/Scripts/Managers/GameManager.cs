using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager
{
    public float damage = 50;
    public int gage = 0;
    public bool GetHit = false;
    public int roundEnemy; // 적을 처치할때마다 줄어들게.
    public bool SkillAniReset = false;//모든 스킬관련 ani 강제 종료

    public int[] roundEnemyCount = new int[14];// GameGround마다의 적 숫자 설정

    public bool isHit = false;

    //게임 상태를 나눠서 상태에 따라 스크립트들이 돌아가게 함
    public enum GameState
    {
        CameraMoving,
        Battle,
        Store,
        Bless,

    }
    public enum GameGround
    {
        R4,R2,K1,K4,L4,G4,Q3,F2,E3, E2,B1,A2,C3,C8
    }
    public GameGround currentGround;
    public GameState currentState;
    //플레이어 죽을 때 실행시킬 함수
    public void PlayerDied()
    {
       
    }

    public void ReSet()
    {

    }

    //인게임 데이터 초기화 
    public void GameStart()
    {
        currentState = GameState.Battle;
        currentGround = GameGround.R4;
        gage = 0;
        GetHit = false;
        Time.timeScale = 1;
        SkillAniReset = true;
        isHit = false;
        damage = 50;
    }

    public void GoJump()
    {
        // 현재 Ground의 인덱스를 가져오기
        int currentIndex = (int)currentGround;

        // Enum의 전체 요소 개수 가져오기
        int groundCount = System.Enum.GetValues(typeof(GameGround)).Length;

        // 다음 Ground 설정 (마지막이면 처음으로 순환)
        int nextIndex = (currentIndex + 1) % groundCount;

        // Enum을 인덱스로 변경하여 설정
        currentGround = (GameGround)nextIndex;

        Debug.Log($"현재 라운드는 {currentGround} 입니다.");
    }

    public void UltimateDamageUp()
    {
        damage = 1000;
    }

    public bool CheckNextRound()
    {
        if(roundEnemyCount[(int)currentGround] ==0) // 적의 수가 0 이면 true
        {
            return true; 
        }
        return false; 
    }
    public void EnemyDied()
    {
        roundEnemyCount[(int)currentGround] -= 1; // 적이죽으면 감지해서 하나 줄이기
    }
}
