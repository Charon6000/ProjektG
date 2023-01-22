using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

	// public TextMeshProUGUI nameText;
	string dialogueText;
	public float dialogueSpeed;
	public UIInformer inf;
	// public TextAsset txtJson;
	// public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{

		
		inf.changeName(dialogue.name);
		inf.stopPlayer();

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			// Debug.Log(sentence);
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText = "";
		inf.changeDialogue(dialogueText);
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText += letter;
			inf.changeDialogue(dialogueText);
			yield return new WaitForSeconds(dialogueSpeed);
			yield return null;
		}
		yield return null;
	}

	void EndDialogue()
	{
		inf.endDialogue();
	}

}