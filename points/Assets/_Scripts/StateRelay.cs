using UnityEngine;
using System.Collections;

public class StateRelay : Singleton<StateRelay> {
	byte[] canvas;
	int[] points;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		if (Network.isClient) {
			Client.Instance.GameInitialized(this);
		}
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {

	}
}
