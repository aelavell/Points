using UnityEngine;
using System.Collections;

public class PointRelay : MonoBehaviour {
	byte[] canvas;
	int[] points;

	void OnNetworkInstantiate(NetworkMessageInfo info) {

	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		if (stream.isWriting) {
			//stream.Serialize(ref canvas);
			//stream.Serialize(ref points);
		}
		else {
			//stream.Serialize(ref canvas);
			//stream.Serialize(ref points);
		}
	}
}
