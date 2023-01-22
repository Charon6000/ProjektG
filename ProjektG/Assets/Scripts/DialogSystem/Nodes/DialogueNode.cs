using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode {

	[Input] public int entry;
	[Output] public int exit;
	public string speakerName;
	[TextArea(3, 10)]
	public string dialugueLine;
	
	public override string GetString()
	{
		return "DialogueNode/" + speakerName + "/" + dialugueLine;
	}
	//to tylko po to by errory nie wkurzały
	public override object GetValue(NodePort port) {
        return null;
    }
}