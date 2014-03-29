using UnityEngine;
using System;
using System.Collections;

public class GlobalEvents {
	public static Action stateRelayCreated;
	public static Action<CommandRelay, NetworkPlayer> commandRelayCreated;
}
