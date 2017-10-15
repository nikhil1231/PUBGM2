using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	NetworkManager nm;

	// Use this for initialization
	void Start () {
		nm = GameObject.FindObjectOfType<NetworkManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setName(){
		nm.Connect();
		GameObject.Find ("Camera").transform.Translate(new Vector3(200,0,0));
	}

	public void joinRoom() {
		nm.JoinRoom();
	}

	public void createRoom(){
		nm.CreateRoom();
	}
}
