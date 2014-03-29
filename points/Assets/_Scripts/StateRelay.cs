using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateRelay : Singleton<StateRelay> {
	byte[] canvas;
	int[] points;

	public State state = State.init;

	public Action enterPlayState;
	public Action enterVictoryState;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		if (GlobalEvents.stateRelayCreated != null) GlobalEvents.stateRelayCreated();
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {

	}

	public void EnterPlayState() {
		networkView.RPC("_EnterPlayState", RPCMode.All, null);
	}

	[RPC] 
	public void _EnterPlayState() {
		if (enterPlayState != null) enterPlayState();
		state = State.play;
	}

	public void EnterVictoryState() {
		networkView.RPC("_EnterVictoryState", RPCMode.All, null);
	}
	
	[RPC]
	public void _EnterVictoryState() {
		if (enterVictoryState != null) enterVictoryState();
		state = State.victory;
	}
}	

public enum State {
	init,
	play,
	victory,
	pause
}