using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : Photon.MonoBehaviour {

	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			this.correctPlayerPos = (Vector3)stream.ReceiveNext ();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext ();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine) {
			transform.position = Vector3.Lerp (transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp (transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
		}
	}
}
