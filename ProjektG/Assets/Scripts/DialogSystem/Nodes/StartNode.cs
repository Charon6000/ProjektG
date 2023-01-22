using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StartNode : BaseNode {

	[Output] public int exit;

	public override string GetString()
	{
		return "Start";
	}
	//to tylko po to by errory nie wkurzały
	public override object GetValue(NodePort port) {
        return null;
    }
}