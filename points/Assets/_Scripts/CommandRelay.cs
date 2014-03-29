using UnityEngine;
using System.Collections;

public class CommandRelay : MonoBehaviour {
	public byte teamIndex;
	public KeyCode[] keys;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		if (Network.isServer) {
			Server.Instance.RegisterCommandRelay(this, info.sender);
		}
		else {
			// delete
		}
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