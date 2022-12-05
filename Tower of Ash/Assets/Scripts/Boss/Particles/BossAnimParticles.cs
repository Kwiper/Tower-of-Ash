using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimParticles : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Boss boss;

    #region Movement Particles
    [SerializeField] GameObject moveParticleContainer;
    [SerializeField] GameObject jumpParticleContainer;
    #endregion

    #region Attack Particles
    [SerializeField] GameObject attackParticleContainer;
    [SerializeField] GameObject jumpAttackParticleContainer;

    [SerializeField] GameObject lungeChargeParticleContainer;
    [SerializeField] GameObject lungeParticleContainer;

    [SerializeField] GameObject bulletHellBurstParticleContainer;
    [SerializeField] GameObject bulletHellFallParticleContainer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void triggerMoveParticle(){
        GameObject moveParticle = Instantiate(moveParticleContainer, transform);
        moveParticle.transform.position = new Vector3(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y+.1f, transform.position.z);
        moveParticle.GetComponent<ParticleSystem>().Play();
        Destroy(moveParticle, 1f);
    }

    public virtual void triggerJumpParticle(){
        GameObject jumpParticle = Instantiate(jumpParticleContainer, transform);
        jumpParticle.transform.position = new Vector3(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y+.1f, transform.position.z);
        jumpParticle.GetComponent<ParticleSystem>().Play();
        Destroy(jumpParticle, 1f);
    }
}
