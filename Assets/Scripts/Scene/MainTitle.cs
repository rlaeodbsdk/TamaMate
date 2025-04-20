using UnityEngine;
using UnityEngine.EventSystems;
using static LoadingScene;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class MainTitle : BaseScene
//MainTitle 클래스는 메인 메뉴 화면을 담당하는 클래스라고 할 수 있습니다. 마우스 클릭이나 터치를 감지하여 컷씬 UI를 보여주거나 주석 처리된 부분처럼 다른 씬으로 전환하는 역할을 수행할 수 있습니다. 

{
    public Sprite[] sprites;
    public Image uiImage;
    public Transform[] GroundRec;
    public float GroundSpeed = 19;

    public Transform[] TreeRec;
    public float TreeSpeed = 10;

    public Transform[] BuildingRec;
    public float BuildSpeed = 5;

    public GameObject esterEggCat;
    bool esterIn = false;

    public float resetPositionX = -35.84f; // 왼쪽으로 이동했을 때 재배치 위치
    public float startPositionOffset = 23.62f; // 새로운 위치 설정 시 기준 오프셋


    void Start()
    {
        StartCoroutine(PlaySpriteAnimation());
         Init();
        foreach (Transform ground in GroundRec)
        {
            MoveGround(ground,"ground");
        }

        foreach (Transform tree in TreeRec)
        {
            MoveGround(tree,"tree");
        }

        foreach (Transform building in BuildingRec)
        {
            MoveGround(building,"building");
        }
    }

    void MoveGround(Transform obj,string St)
    {
        float speed;
        if (St == "ground")
        {
            speed = GroundSpeed;
        }
        else if (St == "tree")
        {
            speed = TreeSpeed;
        }
        else if (St == "building")
        {
            speed = BuildSpeed;
        }
        else { speed = 0; }
        obj.DOMoveX(resetPositionX, speed, false)
            .SetSpeedBased() // 속도 기반 이동 (시간이 아니라 속도로 설정)
            .SetEase(Ease.Linear) // 일정한 속도로 이동
            .OnComplete(() =>
            {
                // 가장 오른쪽에 있는 오브젝트 찾기
                Transform lastGround = GetFarthestRightObj(St);

                // 🔹 부동소수점 오차 방지: 위치를 반올림하여 정확히 맞추기
                float newX;
                if (St == "ground")
                {
                    newX = Mathf.Round(lastGround.position.x + startPositionOffset * 10f) / 10f;
                    if(Random.Range(0,100)<=10)
                    {
                        EsterEggOn();
                    }
                }
                else if (St == "tree")
                {
                    newX = Mathf.Round(lastGround.position.x + 30 * 10f) / 10f;
                }
                else if (St == "building")
                {
                    newX = Mathf.Round(lastGround.position.x + 40 * 10f) / 10f;
                }
                else { newX = 0; }
                obj.position = new Vector2(newX, obj.position.y);


                // 다시 이동 시작
                MoveGround(obj,St);
            });
    }

    Transform GetFarthestRightObj(string St)
    {
        Transform farthest;

        if (St == "ground")
        {
            farthest = GroundRec[0];

            foreach (Transform ground in GroundRec)
            {
                if (ground.position.x > farthest.position.x)
                    farthest = ground;
            }
        }
        else if (St == "tree")
        {
            farthest = TreeRec[0];

            foreach (Transform tree in TreeRec)
            {
                if (tree.position.x > farthest.position.x)
                    farthest = tree;
            }
        }
        else if (St == "building")
        {
            farthest = BuildingRec[0];

            foreach (Transform building in BuildingRec)
            {
                if (building.position.x > farthest.position.x)
                    farthest = building;
            }
        }
        else farthest = null;

        return farthest;
    }

    void EsterEggOn()
    {
        esterEggCat.SetActive(true);
        esterEggCat.transform.DOMoveX(resetPositionX, 12, false)
           .SetSpeedBased() // 속도 기반 이동 (시간이 아니라 속도로 설정)
           .SetEase(Ease.Linear) // 일정한 속도로 이동
           .OnComplete(() => {
               esterEggCat.transform.position = new Vector3(30, -1.51f);
               esterEggCat.SetActive(false); });
    }



IEnumerator PlaySpriteAnimation()
    {

        for (int i = 0; i < sprites.Length; i++)
        {
            uiImage.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(0.1f);
        }
        StartCoroutine(PlaySpriteAnimation());

    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    protected override void Init()
    {
        base.Init();
    }


    public void IsStart()
    {
        LoadingScene.Instance.GoLoading("TitleScene");
    }
}
