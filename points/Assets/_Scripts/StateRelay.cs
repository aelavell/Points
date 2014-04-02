using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateRelay : Singleton<StateRelay> {
	public State state = State.init;
	public Action enterPlayState;
	public Action enterVictoryState;
	public char[] canvas;
	public int[] pointsPerTeam;
	public int[] allTimePointsPerTeam;

	int firstPoint;
	void Start() {
		canvas = new char[Mix.Instance.CanvasSize * Mix.Instance.CanvasSize];
		ClearCanvas();
		pointsPerTeam = new int[4] {0, 0, 0, 0};
		ReadAllTimePoints();
	}

	void ReadAllTimePoints() {
		if (Network.isServer) {
			allTimePointsPerTeam = new int[4];
			for (int i = 0; i < 4; i++) {
				allTimePointsPerTeam[i] = PlayerPrefs.GetInt("allTimePoints" + i.ToString());
			}
		}
		else {
			allTimePointsPerTeam = new int[4] {0, 0, 0, 0};
		}
	}

	void WriteAllTimePoints() {
		if (Network.isServer) {
			for (int i = 0; i < 4; i++) {
				PlayerPrefs.SetInt("allTimePoints" + i.ToString(), allTimePointsPerTeam[i]);
			}
		}
	}

	public void UpdatePoints(byte teamIndex, int points) {
		if (Network.isServer && points != pointsPerTeam[teamIndex]) {
			var pointsAcquired = points - pointsPerTeam[teamIndex];

			for (int i = 0; i < pointsAcquired; i++) {
				var couldAdd = AddPointToCanvas(teamIndex);
				if (!couldAdd) {
					ClearCanvas();
					EnterVictoryState();
				}
			}
			pointsPerTeam[teamIndex] = points;
			if (pointsAcquired >= 0) {
				// if negative points have been acquired, it means the round points were reset,
				// don't want to add to all time points
				allTimePointsPerTeam[teamIndex] += pointsAcquired;

			}
			else {
				ClearCanvas();
			}
		}
	}

	// TODO: refactor to AddPointSSS
	public bool AddPointToCanvas(byte teamIndex) {
		var index = ChooseRandomFreeIndex();
		if (index > -1) {
			canvas[index] = (char)teamIndex;
			return true;
		}
		else {
			return false;
		}
	}

	int ChooseRandomFreeIndex() {
		var freeIndices = new List<int>();
		for (int i = 0; i < canvas.Length; i++) {
			if (canvas[i] == Mix.Instance.BlankColorIndex) {
				freeIndices.Add(i);
			}
		}

		if (freeIndices.Count > 0) return freeIndices[UnityEngine.Random.Range(0, freeIndices.Count)];
		else return -1;
	}

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		if (GlobalEvents.stateRelayCreated != null) GlobalEvents.stateRelayCreated();
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		try {
			stream.Serialize(ref pointsPerTeam[0]);
			stream.Serialize(ref pointsPerTeam[1]);
			stream.Serialize(ref pointsPerTeam[2]);
			stream.Serialize(ref pointsPerTeam[3]);
			// start lazy
			stream.Serialize(ref allTimePointsPerTeam[0]);
			stream.Serialize(ref allTimePointsPerTeam[1]);
			stream.Serialize(ref allTimePointsPerTeam[2]);
			stream.Serialize(ref allTimePointsPerTeam[3]);
			// end lazy
			for (int i = 0; i < Mathf.Pow(Mix.Instance.CanvasSize, 2); i++) {
				stream.Serialize(ref canvas[i]);
			}
		}
		catch {
			// Do nothing, this error state should only happen right at the beginning
		}
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
		WriteAllTimePoints();
	}
	
	[ContextMenu("Generate")]
	public void GenerateRandomCanvas() {
		for (int i = 0; i < Mathf.Pow(Mix.Instance.CanvasSize, 2); i++) {
			canvas[i] = (char)UnityEngine.Random.Range(0, 5);
		}
	}
	
	[ContextMenu("Clear")]
	public void ClearCanvas() {
		for (int i = 0; i < Mathf.Pow(Mix.Instance.CanvasSize, 2); i++) {
			canvas[i] = (char)Mix.Instance.BlankColorIndex;
		}
	}
}	

public enum State {
	init,
	play,
	victory,
	pause
}