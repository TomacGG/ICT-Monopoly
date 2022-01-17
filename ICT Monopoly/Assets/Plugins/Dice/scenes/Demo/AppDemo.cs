using UnityEngine;
using System.Collections;

// Demo application script
public class AppDemo : MonoBehaviour {
	private const int MODE_ROLL = 2;
	private int mode = 0;

	// Use this for initialization
	void Start () {
		SetMode(MODE_ROLL);
	}	
	
	private void SetMode(int pMode)
	{
		mode = pMode;
	}

	void Update () {

	}
	
}
