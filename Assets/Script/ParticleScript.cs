using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private void OnParticleCollision(GameObject obj)
    {
        Destroy(gameObject);

        Debug.Log("è’ìÀ");
    }
}
