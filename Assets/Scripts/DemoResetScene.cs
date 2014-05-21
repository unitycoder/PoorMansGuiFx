using UnityEngine;
using System.Collections;

public class DemoResetScene : MonoBehaviour {


	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown("r")) Application.LoadLevel(0);

	}
}
