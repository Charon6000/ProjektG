using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class NodePasser : MonoBehaviour
{
    public DialogueGraph graph;
    Coroutine _parser,_writer;
    
    public UIInformer inf;
    public float dialogueSpeed;
    bool skipToNext = true;
    bool dialogueIsDone = true;
    string aktLine;
    public string odp = "";
    //to wywołuje trigger
    public void StartDialogue(DialogueGraph _graph)
    {
        graph = _graph;
        foreach (BaseNode b in graph.nodes)
        {
            if(b.GetString() == "Start")
            
            {
                graph.current = b;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }
    //Przetwarza dane w nodzie
    IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');
        switch (dataParts[0])
        {
            case "Start":
                nextNode("exit");
                inf.stopPlayer();
                break;
            case "DialogueNode":
                inf.changeName(dataParts[1]);
                aktLine = dataParts[2];
                _writer = StartCoroutine(TypeSentence(aktLine));
                //czeka aż może sam skipnąć do następnej linijki
                yield return new WaitUntil(() => skipToNext == true);
                nextNode("exit");
                break;
            case "End":
                inf.endDialogue();
                break;
            case "DecisionNode":
                // Debug.Log("Trigger a decision");
                inf.changeName(dataParts[1]);
                aktLine = dataParts[2];
                _writer = StartCoroutine(TypeSentence(aktLine));
                //czeka aż dialog się wypowie i potem daje mu opcje do wyboru
                yield return new WaitUntil(() => dialogueIsDone == true);
                inf.makeDecision(dataParts[2],dataParts[3],dataParts[4]);
                yield return new WaitUntil(() => odp != "");
                nextNode("exit" + odp);
                odp = "";
                break;
        }
        yield return null;
    }
    //Przechodzi między nodami
    public void nextNode(string fieldName)
    {
        if(_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }
        foreach (NodePort p in graph.current.Ports)
        {
            if(p.fieldName == fieldName)
            {
                graph.current =p.Connection.node as BaseNode;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());

    }
    //Wypisuje dialog
    IEnumerator TypeSentence (string sentence)
	{
        skipToNext = false;
        dialogueIsDone = false;
		string dialogueText = "";
		inf.changeDialogue(dialogueText);
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText += letter;
			inf.changeDialogue(dialogueText);
			yield return new WaitForSeconds(dialogueSpeed);
		}
        dialogueIsDone = true;
		yield return null;
	}
    public void playerInterrupts()
    {
        if (dialogueIsDone == true)
        {
            skipToNext = true;
            nextNode("exit");
            return;
        }
        if(dialogueIsDone == false) 
        {
            StopCoroutine(_writer);
            inf.changeDialogue(aktLine);
            dialogueIsDone = true;
            return;
        }
        inf.endDialogue();
    }
}
