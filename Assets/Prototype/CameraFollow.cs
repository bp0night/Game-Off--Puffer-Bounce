using System;
using UnityEngine;

namespace Prototype{
	public class CameraFollow : MonoBehaviour{
		[SerializeField] private Transform target;


		private void LateUpdate(){
			var position = transform.position;
			var targetOffset = Vector3.Slerp(position, new Vector3(position.x, target.position.y, position.z),
				Time.fixedDeltaTime);
			transform.position = targetOffset;
		}
	}
}