using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Server : Singleton<Server> {
	public StateRelay stateRelayPrefab;
	public CommandRelay commandRelayPrefab;
	public List<CommandRelay> commandRelays;

	int port = 25190;
	List<NetworkPlayer> players;
	int clientReadyCount;

	void Start() {
		GlobalEvents.commandRelayCreated += RegisterCommandRelay;

		Network.InitializeServer(Mix.Instance.requiredNumPlayers, port, false);
		players = new List<NetworkPlayer>();	
		commandRelays = new List<CommandRelay>();
	}

	void OnServerInitialized() {
		Network.Instantiate(stateRelayPrefab, Vector3.zero, Quaternion.identity, 0);
		StateRelay.Instance.enterVictoryState += () => clientReadyCount = 0;
	}

	void OnPlayerConnected(NetworkPlayer player) {
		if (StateRelay.Instance.state == State.init) {
			players.Add(player);
		}
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		players.Remove(player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);

	}

	public void RegisterCommandRelay(CommandRelay commandRelay, NetworkPlayer sender) {
		commandRelays.Add(commandRelay);
		if (commandRelays.Count == Mix.Instance.requiredNumPlayers) {
			StateRelay.Instance.EnterPlayState();
		}
	}

	public void ClientIsReady() {
		if (StateRelay.Instance.state == State.victory) {
			clientReadyCount++;
			if (clientReadyCount == Mix.Instance.requiredNumPlayers) {
				StateRelay.Instance.EnterPlayState();
			}
		}
	}
}