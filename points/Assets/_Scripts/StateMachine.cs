using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class StateMachine : MonoBehaviour {
	[SerializeField] private StateMachineState state;
	public StateMachineState State {
		get { return state; }
		private set { state = value; }
	}
	
	[SerializeField] private bool _active = true;
	public bool Active {
		get { return _active; }
		set { _active = value; }
	}
	
	public bool ChangeState(StateMachineState to) {
		bool changedState;
		if (CanChangeState(to)) {
			var from = State;
			if (from) from.OnExit();
			State = to;
	        if (to) to.OnEnter();
			changedState = true;
		}
		else {
			changedState = false;
		}
		
        return changedState;
	}
	
	public bool CanChangeState(StateMachineState to) {
		bool result;
		
		if (Active) {
			if (to == null) result = false;
			else result = true;
		}
		else {
			result = false;
		}
		
		return result;
	}
}