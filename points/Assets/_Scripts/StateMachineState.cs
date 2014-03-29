using UnityEngine;
using System;
using System.Collections;

public class StateMachineState : MonoBehaviour {
	private StateMachine machine;
    public StateMachine Machine {
		get { 
			if (machine == null) {
				// Move up the transform hieararchy until we find a statemachine
				var p = transform.parent;
				while (p) {
					machine = p.GetComponent<StateMachine>();
					if (machine != null) break;	 // found a StateMachine
					else p = p.parent; // if this is null the loop will end
				}
			}
			return machine; 
		}
		set { machine = value; }
	}
	
	[SerializeField] private StateMachineState exitTo;
	public StateMachineState ExitTo {
		get { return exitTo; }
		set { exitTo = value; }
	}
	
  	public bool Active {
        get { return Machine.Active && Machine.State == this; }
    }
	
    public event Action Enter;
    public event Action Exit;

    public void OnEnter() {
		if (Enter != null) Enter();
        if (!Active) {
            //Debug.LogWarning("state machine enter was called after leaving state. odd, but possibly okay"); 
            
			if (Exit != null) Exit();
        }
    }

    public void OnExit() {
        if (Active) if (Exit != null) Exit();
        else {
            //Debug.LogWarning("state machine exit was called after leaving state. odd, but possibly okay" + gameObject.name); 
        }
    }

    [ContextMenu("Switch To")]
    public bool SwitchTo() {
        return Machine.ChangeState(this);
    }

    [ContextMenu("Switch From")]
    public bool SwitchFrom() {
		if (!Active) { return false; }
        if (exitTo) { return exitTo.SwitchTo(); }
        else { return Machine.ChangeState(null); }
    }

	public bool CanSwitchTo() {
		return Machine.CanChangeState(this);
	}
}