using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DecisionNode : BaseNode {

	[Input] public int entry;
	[Output] public int exitA;
	[Output] public int exitB;
	public string speakerName;
	[TextArea(3, 10)]
	public string decisionLine;
	public string optionA;
	public string optionB;
	public override string GetString()
	{
		return "DecisionNode/" + speakerName + "/" + decisionLine + '/' + optionA + '/' + optionB;
	}
	//to tylko po to by errory nie wkurzały
	public override object GetValue(NodePort port) {
        return null;
    }
}