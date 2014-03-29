using UnityEngine;
using System.Collections;

public class Client : Singleton<Client> {
	string ip = "127.0.0.1";
	int port = 25190;
	public byte teamIndex;
	public StateRelay stateRelayPrefab; 
	public CommandRelay commandRelayPrefab;
	CommandRelay commandRelay;
	
	void Start() {
		GlobalEvents.stateRelayCreated += CreateCommandRelay;
		Network.Connect(ip, port);
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		Network.Connect(ip, port);
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Network.Connect(ip, port);
	}
	
	public void CreateCommandRelay() {
		commandRelay = Network.Instantiate(commandRelayPrefab, Vector3.zero, Quaternion.identity, 0) as CommandRelay;
		commandRelay.teamIndex = (char)teamIndex;
		//PointCanvas.Instance.GenerateRandomImage();
	}
}