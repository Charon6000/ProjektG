using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public DialogueGraph graph;
	public void TriggerDialogue ()
	{
		FindObjectOfType<NodePasser>().StartDialogue(graph);
	}

}