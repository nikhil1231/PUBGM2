using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class NetworkManager : Photon.PunBehaviour {

	public Camera cam1, cam2;
	public Material mat;

	TypedLobby defaultLobby;
	string gameVersion = "1.0";
	string name;
	RoomInfo[] rooms;
	void Update () {
		
	}

	void Start () {
		defaultLobby = new TypedLobby ("default", LobbyType.Default);
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
//		Debug.Log(PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby(){
		Debug.Log ("Joined lobby");
	}

	void OnReceivedRoomListUpdate () {
		GetRoomList ();
	}

	public void Connect () {
		name = GameObject.Find("Menu1").transform.Find("Name").GetComponent<InputField>().text;
		if(name.Length < 2 || name.Length > 16) {
			print("name must be between 2 and 16 characters");
			return;
		}
		PhotonNetwork.ConnectUsingSettings(gameVersion);
	}

	 
	void OnConnectedToMaster() {
		PhotonNetwork.JoinLobby(defaultLobby);
	}

	public void CreateRoom () {
		string gameName = GameObject.Find("Menu2").transform.Find("Name").GetComponent<InputField>().text;
		PhotonNetwork.CreateRoom(gameName, null, defaultLobby);
	}

	public void GetRoomList () {
		rooms = PhotonNetwork.GetRoomList();
		Debug.Log(rooms.Length);
		Dropdown drop = GameObject.Find ("Menu2").transform.Find ("Dropdown").GetComponent<Dropdown> ();
		drop.ClearOptions();
		List<Dropdown.OptionData> roomNames = new List<Dropdown.OptionData>();
		foreach(RoomInfo room in rooms) {
			if(room.IsOpen) {
				roomNames.Add(new Dropdown.OptionData(room.Name));
			}
		}
		drop.AddOptions (roomNames);
	}

	public void JoinRoom () {
		int selection = GameObject.Find("Menu2").transform.Find("Dropdown").GetComponent<Dropdown>().value;
		string roomName = rooms[selection].Name;
		PhotonNetwork.JoinRoom(roomName);
	}

	void OnJoinedRoom(){
		Debug.Log("Joined room!!");
		GameObject player = PhotonNetwork.Instantiate ("player", new Vector3 (10, 1, 1), Quaternion.identity, 0);
		GameObject playerCam = PhotonNetwork.Instantiate ("CameraTarget", new Vector3 (10, 1, 1), Quaternion.identity, 0);
		player.GetComponent<Movement>().enabled = true;
		player.GetComponent<Rigidbody>().isKinematic = false;
		if (PhotonNetwork.isMasterClient) {
			player.GetComponent<Renderer> ().material = mat;
		}

		Debug.Log ("player transform: " + player.transform);

//		Debug.Log ("player transform: " + player.transform);

		playerCam.GetComponent<CameraTargetScript> ().player = player.transform;

		playerCam.transform.GetChild (0).GetComponent<Camera> ().enabled = true;
		cam1.gameObject.SetActive(false);
//		cam2.enabled = true;
	}
}
