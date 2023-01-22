using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class EndNode : BaseNode {

	[Input] public int entry;

	public override string GetString()
	{
		return "End";
	}
	//to tylko po to by errory nie wkurzały
	public override object GetValue(NodePort port) {
        return null;
    }
}