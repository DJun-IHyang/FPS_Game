using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectTrigger : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    List<ParticleSystem.Particle> touchedParticle = new List<ParticleSystem.Particle>();

    private void OnParticleTrigger()
    {
        ParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, touchedParticle);

        foreach(var p in touchedParticle)
        {
            print("��ƼŬ ����" + p.position);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        print(other.name);
    }
}
