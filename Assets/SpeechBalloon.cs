using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBalloon : UI_Popup
{
    public TMP_Text speechText;
    public Transform ballonTransform;

    private Character character;
    private Transform charTransfom;

    private void Start()
    {
        character = FindFirstObjectByType<Character>();
        charTransfom = character.GetComponentInParent<Transform>();

        string text = "�ȳ��ϼ��� �� ���� ����ּ���";
        SetText(text);
    }

    void SetText(string text)
    {
        speechText.text = text;
    }

    private void FixedUpdate()
    {
        ballonTransform.position = charTransfom.position;
    }
}
