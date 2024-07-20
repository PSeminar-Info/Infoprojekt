using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTimer : MonoBehaviour {
	public Text timerText;

	private void Update() {
		var time = TimeSpan.FromSeconds(Time.time);
		timerText.text = "Zeit: " + time.ToString("mm':'ss'.'ff");
	}
}