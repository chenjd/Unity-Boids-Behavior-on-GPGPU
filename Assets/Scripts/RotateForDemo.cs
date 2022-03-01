using UnityEngine;

public class RotateForDemo : MonoBehaviour
{

	private Transform _transform;

	private void Start ()
	{
		_transform = transform;
	}
	
	private void Update () {
        _transform.localRotation = Quaternion.AngleAxis(10 * Time.deltaTime, Vector3.up) * _transform.localRotation;
	}
}
