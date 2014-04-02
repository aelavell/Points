using UnityEngine;
using System;
using System.Collections;

public class Client : Singleton<Client> {
	string ip = "127.0.0.1";
	int port = 25190;
	//public byte teamIndex;
	public StateRelay stateRelayPrefab; 
	public CommandRelay commandRelayPrefab;
	public CommandRelay commandRelay;
	public UILabel connectionIndicator;
	bool connected;
	
	void Start() {
		GlobalEvents.stateRelayCreated += CreateCommandRelay;
		StartCoroutine(TryConnect());
	}

	void OnGUI() {
		/*
		if (!connected) {
			ip = GUI.TextField(new Rect(Screen.width / 2, 0, 200, 30), ip);
			//if (GUI.Button(new Rect(Screen.width / 2, 60, 60, 60), "Connect")) {
				//Network.Connect(ip, port);	
			//}
		}
		else {
			if (GUI.Button(new Rect(Screen.width / 2, 60, 60, 60), "Disconnect")) {
				Network.Disconnect();	
			}
		}
		*/
	}
	
	IEnumerator TryConnect() {
		yield return new WaitForSeconds(0.5f);
		Network.Connect(ip, port);
	}

	void OnConnectedToServer() {
		connected = true;
		connectionIndicator.enabled = false;
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		//Network.Connect(ip, port);
		StartCoroutine(TryConnect());
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		//Network.Connect(ip, port);
		Destroy(commandRelay.gameObject);
		Destroy(StateRelay.Instance.gameObject);
		connected = false;
		connectionIndicator.enabled = true;
		PointCanvas.Instance.ClearDisplay();
		StartCoroutine(TryConnect());
	}
	
	public void CreateCommandRelay() {
	    commandRelay = Network.Instantiate(commandRelayPrefab, Vector3.zero, Quaternion.identity, 0) as CommandRelay;
	}
}