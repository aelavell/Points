using UnityEngine;
using System.Collections;

public class VictoryScene : MonoBehaviour {
	public Animator animator;
	public Animator poinze;
	public UILabel thisRoundLabel;

	void Start() {
		GlobalEvents.stateRelayCreated += () => {
			StateRelay.Instance.enterPlayState += () => MasterAudio.PlaySound("points");
			StateRelay.Instance.enterVictoryState += Play;
		};
	
		animator = GetComponent<Animator>();
		animator.enabled = false;
	}
	
	void Play() {
		animator.enabled = true;
		animator.Play("ThisRound");
		poinze.enabled = true;
		poinze.Play("Poinze");
		thisRoundLabel.alpha = 1;
		thisRoundLabel.GetComponent<FlashAllColors>().enabled = true;
	}

	public void WipeRoundPoints() {
		MasterAudio.PlaySound("alltime");
		Client.Instance.commandRelay.points = 0;
		thisRoundLabel.alpha = 0;
		thisRoundLabel.GetComponent<FlashAllColors>().enabled = false;
	}

	public void ThisRound() {
		MasterAudio.PlaySound("thisround");
	}

	public void ReadyToPlay() {
		Client.Instance.commandRelay.ReadyToPlay();
	}
}