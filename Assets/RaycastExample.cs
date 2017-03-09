using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaycastExample : MonoBehaviour {
	
	public GameObject hudText;
	public Transform enemy;

	public ParticleSystem fireballCast;
	public ParticleSystem fireballProjectile;

	public Slider castBar;

	public Coroutine castCoroutine;

	private bool isCasting = false;

	void Update () {
		
		if (isCasting && 
			(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 )) {
			DisplayHud("Interupted");
			StopAllCoroutines();
			StopCast();
		} else if (!isCasting && Input.GetMouseButtonDown(0)) {
			Ray ray = new Ray(transform.position + Vector3.up, Vector3.forward);

			if (Physics.Linecast(transform.position + Vector3.up, enemy.position + Vector3.up)) {
				DisplayHud("Line of Sight");
			} else if (Mathf.Abs((transform.position - enemy.position).magnitude) > 10) {
				DisplayHud("Out of Range");
			} else {
				castCoroutine = StartCoroutine(CastFireball());
			}
		}
	}

	void DisplayHud(string message) {
		hudText.GetComponent<Text>().text = message;
		hudText.GetComponent<Animator>().SetTrigger("ShowMessage");
	}

	IEnumerator CastFireball() {
		isCasting = true;
		transform.LookAt(enemy.position);
		yield return null;
		fireballCast.Play();
		float timeElapsed = 0;
		castBar.maxValue = 3.0f;
		castBar.gameObject.SetActive(true);
		while (timeElapsed < castBar.maxValue) {
			timeElapsed += Time.deltaTime;
			castBar.value = timeElapsed;
			yield return null;
		}
		StopCast();
		fireballProjectile.Emit(1);
	}

	void StopCast() {
		castBar.gameObject.SetActive(false);
		fireballCast.Stop();
		isCasting = false;
	}
}
