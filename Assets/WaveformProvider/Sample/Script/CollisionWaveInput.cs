using UnityEngine;

namespace Es.WaveformProvider.Sample
{
	/// <summary>
	/// Enter the waveform with a collision.
	/// </summary>
	[RequireComponent(typeof(Collider), typeof(Rigidbody))]
	public class CollisionWaveInput : MonoBehaviour
	{
		[SerializeField]
		private Texture2D waveform;

		[SerializeField, Range(0f, 1f)]
		private float inputScaleFitter = 0.01f;

		[SerializeField, Range(0f, 1f)]
		private float strength = 1f;

		private new Rigidbody rigidbody;

		private void Awake()
		{
			rigidbody = GetComponent<Rigidbody>();
		}

		private void OnTriggerEnter(Collider collision)
		{
			WaveInput(collision);
		}

		public void OnCollisionStay(Collision collision)
		{
			WaveInput(collision);
		}

		private void WaveInput(Collider collision)
		{
			foreach (var p in collision.)
			{
				var canvas = p.otherCollider.GetComponent<WaveConductor>();
				if (canvas != null)
					canvas.Input(waveform, p.point, rigidbody.velocity.magnitude * rigidbody.mass * inputScaleFitter, strength);
			}
		}
	}
}