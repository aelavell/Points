using UnityEngine;
using System.Collections;

public class CommandRelay : MonoBehaviour {
	public byte teamIndex;
	public KeyCode[] keys;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		if (GlobalEvents.commandRelayCreated != null) GlobalEvents.commandRelayCreated(this, info.sender);
	}

	void KeyPressed(byte index) {
		PointCanvas.Instance.AddPoint(index);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {

	}

	void Update() {
		byte i = 0;
		foreach (var key in keys) {
			if (Input.GetKeyDown(key)) {
				KeyPressed((byte)(i+1));
			}
			i++;
		}
	}
}