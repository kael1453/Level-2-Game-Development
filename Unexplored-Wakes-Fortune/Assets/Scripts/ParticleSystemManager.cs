using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public GameObject hitParticlePrefab;
    public static Pool hitParticles;
    // Start is called before the first frame update
    void Start()
    {
        hitParticles = new Pool(hitParticlePrefab, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CreateParticle(Vector3 position, Quaternion rotation)
    {
        hitParticles.ActivateNext(position, rotation);
    }
}
