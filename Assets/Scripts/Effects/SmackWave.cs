using UnityEngine;
using System.Collections;

public class SmackWave : MonoBehaviour {

	[SerializeField]
	public float scaleFactor = 1;

	[SerializeField]
	public float deathThreshold = .01f;

	[SerializeField]
	public bool terminateSelf = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = new Vector3(this.transform.localScale.x + scaleFactor, this.transform.localScale.y - scaleFactor, this.transform.localScale.z + scaleFactor);

		if(this.transform.localScale.y <= deathThreshold && terminateSelf)
		{
			Destroy(this.gameObject);

		}
    }
}
