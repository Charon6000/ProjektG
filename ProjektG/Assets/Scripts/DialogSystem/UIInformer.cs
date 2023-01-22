using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NotifyAboutTxt(string n);
public delegate void EndTheDialogue();
public delegate void DecisionActivation(DecisionArgs d);
[CreateAssetMenu(fileName = "New uiInformer", menuName = "Informer")]
public class UIInformer : ScriptableObject
{
    public event NotifyAboutTxt nameChange;
    public event NotifyAboutTxt dialogueChange;
    public event EndTheDialogue dialogueEnd;
    public event DecisionActivation decisionMake;

    public void changeName(string _name)
    {
        nameChange?.Invoke(_name);

    }
    public void stopPlayer()
    {
        OWController con = GameObject.Find("Player").GetComponent<OWController>();
        con.canMove = false;
        con.inDialogue = true;
    }
    public void changeDialogue(string _dialogue)
    {
        dialogueChange?.Invoke(_dialogue);
    }
    public void endDialogue()
    {
        dialogueEnd?.Invoke();
        OWController con = GameObject.Find("Player").GetComponent<OWController>();
        con.canMove = true;
        con.inDialogue = false;
    }
    public void makeDecision(string question, string optionA, string optionB)
    {
        DecisionArgs d = new DecisionArgs(question, optionA, optionB);
        decisionMake?.Invoke(d);
    }
    
}
public struct DecisionArgs
    {
        public DecisionArgs(string question, string optionA, string optionB)
        {
            Question = question;
            OptionA = optionA;
            OptionB = optionB;
        }
        public string Question { get;}
        public string OptionA  { get;}
        public string OptionB  { get;}
    }