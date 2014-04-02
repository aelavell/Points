using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Server : Singleton<Server> {
	public StateRelay stateRelayPrefab;
	public CommandRelay commandRelayPrefab;
	public List<CommandRelay> commandRelays;
	Dictionary <NetworkPlayer, CommandRelay> commandRelaysForPlayers;

	int port = 25190;
	int clientReadyCount;

	void Start() {
		GlobalEvents.commandRelayCreated += RegisterCommandRelay;

		Network.InitializeServer(Mix.Instance.requiredNumPlayers, port, false);	
		commandRelays = new List<CommandRelay>();
		commandRelaysForPlayers = new Dictionary<NetworkPlayer, CommandRelay>();
	}

	void OnServerInitialized() {
		Network.Instantiate(stateRelayPrefab, Vector3.zero, Quaternion.identity, 0);
		StateRelay.Instance.enterVictoryState += () => clientReadyCount = 0;
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		commandRelays.Remove(commandRelaysForPlayers[player]);
		commandRelaysForPlayers.Remove(player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}

	public void RegisterCommandRelay(CommandRelay commandRelay, NetworkPlayer sender) {
		commandRelays.Add(commandRelay);
		commandRelaysForPlayers.Add(sender, commandRelay);
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