using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Server : Singleton<Server> {
	int port = 25190;
	int requiredNumPlayers = 1;
	List<NetworkPlayer> players;
	List<CommandRelay> commandRelays;
	public StateRelay stateRelayPrefab;
	StateRelay stateRelay;


	void Start() {
		Network.InitializeServer(requiredNumPlayers, port, false);
		players = new List<NetworkPlayer>();
		commandRelays = new List<CommandRelay>();
	}

	void OnServerInitialized() {
		Debug.Log("server is running");
	}

	void OnPlayerConnected(NetworkPlayer player) {
		players.Add(player);
		if (players.Count == requiredNumPlayers) {
			InitializeGame();
		}
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		players.Remove(player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}

	void InitializeGame() {
		stateRelay = Network.Instantiate(stateRelayPrefab, Vector3.zero, Quaternion.identity, 0) as StateRelay;
	}

	public void RegisterCommandRelay(CommandRelay commandRelay, NetworkPlayer sender) {
		commandRelays.Add(commandRelay);
		if (commandRelays.Count == requiredNumPlayers) {
			StartGame();
		}
	}

	void StartGame() {

	}
}