using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class MatchMaker : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("1.0");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
//		Debug.Log(PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby(){
		Debug.Log("Joined lobby, joining random room...");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("Can't join random room");
		PhotonNetwork.CreateRoom (null);
	}

	void OnPhotonJoinedRoom(){
		Debug.Log("Joined room!!");
		GameObject player = PhotonNetwork.Instantiate ("player", new Vector3 (10, 1, 1), Quaternion.identity, 0);
	}

	void onConnectedToMaster(){
		Debug.Log ("Connected");
	}
}
