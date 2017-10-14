using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class NetworkManager : Photon.PunBehaviour {

	string gameVersion = "1.0";
	string name;
	RoomInfo[] rooms;
	void Update () {
		
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
//		Debug.Log(PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby(){
		Debug.Log("Joined lobby, joining random room...");
	}

	public void Connect () {
		name = GameObject.Find("Menu1").transform.Find("Name").GetComponent<InputField>().text;
		if(name.Length < 2 || name.Length > 16) {
			print("name must be between 2 and 16 characters");
			return;
		}	
		PhotonNetwork.ConnectUsingSettings(gameVersion);
	}

	public void CreateRoom () {
		string gameName = GameObject.Find("Menu2").transform.Find("GameName").GetComponent<InputField>().text;
		PhotonNetwork.CreateRoom(gameName);
	}

	public void GetRoomList () {
		rooms = PhotonNetwork.GetRoomList();
		GameObject.Find("Menu2").transform.Find("Dropdown").GetComponent<Dropdown>().ClearOptions();
		List<string> roomNames = new List<string>();
		foreach(RoomInfo room in rooms) {
			if(room.IsOpen) {
				roomNames.Add(room.name);
			}
		}
	}

	public void JoinRoom () {
		int selection = GameObject.Find("Menu2").transform.Find("Dropdown").GetComponent<Dropdown>().value;
		string roomName = rooms[selection].name;
		PhotonNetwork.JoinRoom(roomName);
	}

	void OnPhotonJoinedRoom(){
		Debug.Log("Joined room!!");
		GameObject player = PhotonNetwork.Instantiate ("player", new Vector3 (10, 1, 1), Quaternion.identity, 0);
	}
}
