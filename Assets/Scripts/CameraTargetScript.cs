using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetScript : Photon.MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			transform.position = player.position;
		}
	}
}
