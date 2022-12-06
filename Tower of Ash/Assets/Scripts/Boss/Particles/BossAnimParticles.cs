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

    [SerializeField] GameObject pillarChargeParticleContainer;
    [SerializeField] GameObject pillarCrushParticleContainer;

    [SerializeField] GameObject fireballStabParticleContainer;
    [SerializeField] GameObject fireballLaunchParticleContainer;

    [SerializeField] GameObject lungeChargeParticleContainer;
    [SerializeField] GameObject lungeParticleContainer;

    [SerializeField] GameObject bulletHellChargeParticleContainer;
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
    
    //movement
    public virtual void triggerMoveParticle(){
        GameObject moveParticle = Instantiate(moveParticleContainer, transform);
        if(boss.FacingDirection == 1) moveParticle.transform.position = new Vector3(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y+.1f, transform.position.z);
        else moveParticle.transform.position = new Vector3(GetComponent<BoxCollider2D>().bounds.max.x, GetComponent<BoxCollider2D>().bounds.min.y+.1f, transform.position.z);
        moveParticle.GetComponent<ParticleSystem>().Play();
        Destroy(moveParticle, 1f);
    }

    public virtual void triggerJumpParticle(){
        GameObject jumpParticle = Instantiate(jumpParticleContainer, transform);
        jumpParticle.transform.position = new Vector3(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y+.1f, transform.position.z);
        jumpParticle.GetComponent<ParticleSystem>().Play();
        Destroy(jumpParticle, 1f);
    }

    //attacks
    public virtual void triggerAttackParticle(){
        GameObject atkParticle = Instantiate(attackParticleContainer, transform);
        atkParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        atkParticle.GetComponent<ParticleSystem>().Play();
        Destroy(atkParticle, 1f);
    }

    public virtual void triggerJumpAttackParticle(){
        GameObject jatkParticle = Instantiate(jumpAttackParticleContainer, transform);
        jatkParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        jatkParticle.GetComponent<ParticleSystem>().Play();
        Destroy(jatkParticle, 1f);
    }

    //pillar anim
    public virtual void triggerPillarChargeParticle(){
        GameObject pchParticle = Instantiate(pillarChargeParticleContainer, transform);
        if(boss.FacingDirection == 1) pchParticle.transform.position = new Vector3(transform.position.x-.8f, transform.position.y+1.7f, transform.position.z);
        else pchParticle.transform.position = new Vector3(transform.position.x+.8f, transform.position.y+1.7f, transform.position.z);
        pchParticle.GetComponent<ParticleSystem>().Play();
        Destroy(pchParticle, 1f);
    }

    public virtual void triggerPillarCrushParticle(){
        GameObject pcParticle = Instantiate(pillarCrushParticleContainer, transform);
        if(boss.FacingDirection == 1) pcParticle.transform.position = new Vector3(transform.position.x-.8f, transform.position.y+1.5f, transform.position.z);
        else pcParticle.transform.position = new Vector3(transform.position.x+.8f, transform.position.y+1.5f, transform.position.z);
        pcParticle.GetComponent<ParticleSystem>().Play();
        Destroy(pcParticle, 1f);
    }

    //fireball anim
    public virtual void triggerFireballStabParticle(){
        GameObject fsParticle = Instantiate(fireballStabParticleContainer, transform);
        fsParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        fsParticle.GetComponent<ParticleSystem>().Play();
        Destroy(fsParticle, 1f);
    }

    public virtual void triggerFireballLaunchParticle(){
        GameObject flParticle = Instantiate(fireballLaunchParticleContainer, transform);
        flParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        flParticle.GetComponent<ParticleSystem>().Play();
        Destroy(flParticle, 1f);
    }

    //lunge anim
    public virtual void triggerLungeChargeParticle(){
        GameObject lcParticle = Instantiate(lungeChargeParticleContainer, transform);
        lcParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        lcParticle.GetComponent<ParticleSystem>().Play();
        Destroy(lcParticle, 1f);
    }

    public virtual void triggerLungeParticle(){
        GameObject lParticle = Instantiate(lungeParticleContainer, transform);
        lParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        lParticle.GetComponent<ParticleSystem>().Play();
        Destroy(lParticle, 1f);
    }

    //final phase
    public virtual void triggerBulletHellChargeParticle(){
        GameObject bhcParticle = Instantiate(bulletHellChargeParticleContainer, transform);
        bhcParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bhcParticle.GetComponent<ParticleSystem>().Play();
        Destroy(bhcParticle, 2.5f);
    }

    public virtual void triggerBulletHellBurstParticle(){
        GameObject bhbParticle = Instantiate(bulletHellBurstParticleContainer, transform);
        bhbParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bhbParticle.GetComponent<ParticleSystem>().Play();
        Destroy(bhbParticle, 1f);
    }

    public virtual void triggerBulletHellFallParticle(){
        GameObject bhfParticle = Instantiate(bulletHellFallParticleContainer, transform);
        bhfParticle.transform.position = new Vector3(transform.position.x, transform.position.y-1f, transform.position.z);
        bhfParticle.GetComponent<ParticleSystem>().Play();
        Destroy(bhfParticle, 1f);
    }
}
