using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DialogueSystem_UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform dialogueUI;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private float dialogueAnimDuration;
    [SerializeField] private Ease dialogueAnimEase;
    [SerializeField] private float typingIntervalBetweenLetters = 0.01f;

    private Vector2 dialogueUIPos, hiddenDialoguePos;
    private bool canCloseUI;
    internal bool isShowingDialogue;
    
    private void Start()
    {
        dialogueUIPos = dialogueUI.anchoredPosition;
        hiddenDialoguePos = dialogueUIPos - new Vector2(0, 500);
    }

    private void Update()
    {
        if (Input.anyKeyDown && canCloseUI)
            HideDialogueUI();
    }

    public void ShowDialogueUI(string dialogue, string hint)
    {
        StopAllCoroutines();
        isShowingDialogue = true;
        canCloseUI = false;
        dialogueText.text = "";
        hintText.text = hint;
        dialogueUI.anchoredPosition = hiddenDialoguePos;
        dialogueUI.gameObject.SetActive(true);
        dialogueUI.DOAnchorPos(dialogueUIPos,dialogueAnimDuration).SetEase(dialogueAnimEase).OnComplete(()=>StartCoroutine(TypeTextInDialogueUI(dialogue)));
    }

    private void HideDialogueUI()
    {
        isShowingDialogue = false;
        dialogueUI.DOAnchorPos(hiddenDialoguePos,dialogueAnimDuration).SetEase(dialogueAnimEase).OnComplete(()=>
        {
            dialogueUI.gameObject.SetActive(false);
            canCloseUI = false;
        });
    }

    IEnumerator TypeTextInDialogueUI(string text)
    {
        dialogueText.text = "";
        foreach (var letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingIntervalBetweenLetters);
        }

        isShowingDialogue = false;

        yield return new WaitForSeconds(1);
        DataReferences.Instance.cinemachineVirtualCamera.Follow = DataReferences.Instance.playerTransform;
        canCloseUI = true;
    }
}
