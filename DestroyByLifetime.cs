using UnityEngine;
using System.Collections;

public class DestroyByLifetime : MonoBehaviour
{
		public float lifetime;

		void Start()
		{
				Destroy(gameObject, lifetime);
		}
}
