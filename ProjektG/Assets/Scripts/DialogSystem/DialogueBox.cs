using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public UIInformer inf;
    public GameObject[] buttons;
    [SerializeField] Animator anim;
    void _changeName(string _name)
    {
        nameText.text = _name;
        if(anim.GetBool("Open") == false)
        {
            anim.SetBool("Open", true);
        }
    }
    void _changeDialogue(string _dialogue)
    {
        dialogueText.text = _dialogue;
    }
    void _endDialogue()
    {
        anim.SetBool("Open", false);
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        inf.nameChange += _changeName;
        inf.dialogueChange += _changeDialogue;
        inf.dialogueEnd +=_endDialogue;
        inf.decisionMake += _triggerDecision;
    }
    private void Start()
    {
        activateButtons(false);
    }
    public void _triggerDecision(DecisionArgs dec)
    {
        activateButtons(true);
        dialogueText.text = dec.Question;
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = dec.OptionA;
        // buttons[0].GetComponent<Button>().onClick.AddListener(makeDecision('A'));
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = dec.OptionB;
    }
    void activateButtons(bool to){
        foreach (var b in buttons)
        {
            b.SetActive(to);
        }
    }
    public void makeDecision(string ans)
    {
        // Debug.Log("tak");
        FindObjectOfType<NodePasser>().odp = ans;
        activateButtons(false);
    }
}
