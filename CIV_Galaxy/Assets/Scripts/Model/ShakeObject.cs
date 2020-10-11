using UnityEngine;
using System.Collections;
using Zenject;

public class ShakeObject : MonoBehaviour
{
	private Transform tr;
	private float elapsed, i_Duration, i_Power, percentComplete;
	private Vector3 originalPos; 
	private IGalaxyUITimer _galaxyUITimer;

	[Inject]
	public void Inject(IGalaxyUITimer galaxyUITimer)
	{
		this._galaxyUITimer = galaxyUITimer;

		percentComplete = 1;
		tr = GetComponent<Transform>();
	}

	public void Shake(float duration, float power)
	{
		if (percentComplete == 1) originalPos = tr.localPosition;
		elapsed = 0;
		i_Duration = duration;
		i_Power = power;

		StopAllCoroutines();
		StartCoroutine(Shake());
	}

	private IEnumerator Shake()
	{
		while (elapsed < i_Duration)
		{
			if (_galaxyUITimer.IsPause == false)
			{
				elapsed += Time.deltaTime;
				percentComplete = elapsed / i_Duration;
				percentComplete = Mathf.Clamp01(percentComplete);
				Vector3 rnd = Random.insideUnitSphere * i_Power * (1f - percentComplete);

				tr.localPosition = originalPos + new Vector3(rnd.x, rnd.y, 0);
			}

			yield return new WaitForFixedUpdate();
		}
	}
}

