using System;
using TarodevController;
using UnityEngine;

namespace Prototype{
	public class BounceMovement : MonoBehaviour{
		[SerializeField] private GameObject bullet;

		private FrameInput _frameInput;
		private Rigidbody2D _rigidbody;
		private readonly Vector2 _maxScale = Vector2.one * 2;
		private readonly Vector2 _minScale = Vector2.one;

		private void Awake(){
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		private void Update(){
			_frameInput = new FrameInput{
				JumpDown = Input.GetButtonUp("Jump"),
				JumpHeld = Input.GetButton("Jump"),
				Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
			};
			HandleJump();
		}

		private void FixedUpdate(){
			HandleScale();
			HandleDirection();
		}

		private void HandleJump(){
			if(_frameInput.JumpDown){
				_rigidbody.AddForce(_frameInput.Move + Vector2.up * (10 * transform.localScale.y), ForceMode2D.Impulse);
			}
		}

		private void HandleScale(){
			if(!_frameInput.JumpHeld){
				return;
			}

			var addedScale = Vector3.Lerp(transform.localScale, _maxScale, Time.deltaTime);
			transform.localScale = addedScale;
		}

		private void HandleDirection(){
			if(_frameInput.Move.x == 0){
				var rigidbodyVelocity = _rigidbody.velocity;
				rigidbodyVelocity.x = Mathf.MoveTowards(rigidbodyVelocity.x, 0, 10 * Time.fixedDeltaTime);
				_rigidbody.velocity = rigidbodyVelocity;
			}
			else{
				var rigidbodyVelocity = _rigidbody.velocity;
				rigidbodyVelocity.x =
						Mathf.MoveTowards(rigidbodyVelocity.x, _frameInput.Move.x * 20, 10 * Time.fixedDeltaTime);
				_rigidbody.velocity = rigidbodyVelocity;
			}
		}

		private void OnCollisionEnter2D(Collision2D col){
			var collisionForce = (col.relativeVelocity / Time.deltaTime).magnitude;
			if(collisionForce < 300){
				return;
			}

			//if force == 500 scale -= 0.3f >>> 
			var deductScale = (collisionForce / 500) * 0.3f * Vector2.one;
			Vector2 currentScale = transform.localScale;
			var deductedScale = currentScale - deductScale;
			if(deductedScale.x < 1 || deductedScale.y < 1){
				transform.localScale = _minScale;
			}
			else{
				transform.localScale = deductedScale;
				Fire(Mathf.RoundToInt(deductScale.x * 10), col.relativeVelocity);
			}
		}

		private void Fire(int bulletCount, Vector3 hitDirection){
			for(var i = 0; i < bulletCount; i++){
				var bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
				var rotation = Quaternion.Euler(0, 0, 30 * i);
				bulletClone.transform.rotation = rotation * Quaternion.LookRotation(Vector3.forward, hitDirection);
				var bulletRigid = bulletClone.GetComponent<Rigidbody2D>();
				bulletRigid.AddForce(bulletClone.transform.up * 10, ForceMode2D.Impulse);
				Destroy(bulletClone, 4.5f);
			}
		}
	}
}