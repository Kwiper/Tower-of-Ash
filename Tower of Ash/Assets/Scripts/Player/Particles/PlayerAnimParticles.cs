using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimParticles : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Player player;

    #region Movement Particles
    [SerializeField] GameObject moveStartParticleContainer;
    [SerializeField] GameObject moveParticleContainer;
    [SerializeField] GameObject wallSlideParticleContainer;
    [SerializeField] GameObject dashParticleContainer;
    #endregion

    #region Attack Particles
    [SerializeField] GameObject attack1ParticleContainer;
    [SerializeField] GameObject attack2ParticleContainer;
    [SerializeField] GameObject attack3ParticleContainer;

    [SerializeField] GameObject upAttackParticleContainer;
    [SerializeField] GameObject sideAirParticleContainer;
    [SerializeField] GameObject downAirParticleContainer;

    [SerializeField] GameObject chargeAttackParticleContainer;
    [SerializeField] GameObject airChargeAttackParticleContainer;
    #endregion

    #region Misc Particles
    [SerializeField] GameObject fireballAnimParticleContainer;
    [SerializeField] GameObject healParticleContainer;
    #endregion

    #region Misc
    private bool moveStartTriggered = false;
    private bool dashTriggered = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("MoveStart")) moveStartTriggered = false;
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Dash")) dashTriggered = false;
    }

    public virtual void triggerMoveStartParticle(){
        if(!moveStartTriggered){
            GameObject msParticle = Instantiate(moveStartParticleContainer, transform);
            msParticle.transform.position = new Vector3(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y+.1f, transform.position.z);
            msParticle.GetComponent<ParticleSystem>().Play();
            Destroy(msParticle, 1f);

            moveStartTriggered = true;
        }
    }

    public virtual void triggerMoveParticle(){
        GameObject moveParticle = Instantiate(moveParticleContainer, transform);
        moveParticle.transform.position = new Vector3(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y+.1f, transform.position.z);
        moveParticle.GetComponent<ParticleSystem>().Play();
        Destroy(moveParticle, 1f);
    }

    public virtual void triggerWallSlideParticle(){
        GameObject wsParticle = Instantiate(wallSlideParticleContainer, transform);
        if(player.FacingDirection == 1) wsParticle.transform.position = new Vector3(GetComponent<BoxCollider2D>().bounds.max.x-.1f, GetComponent<BoxCollider2D>().bounds.max.y, transform.position.z);
        else wsParticle.transform.position = new Vector3(GetComponent<BoxCollider2D>().bounds.min.x+.1f, GetComponent<BoxCollider2D>().bounds.max.y, transform.position.z);
        wsParticle.GetComponent<ParticleSystem>().Play();
        Destroy(wsParticle, 1f);
    }

    public virtual void triggerDashParticle(){
        if(!dashTriggered){
            GameObject dashParticle = Instantiate(dashParticleContainer, transform);
            dashParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            dashParticle.GetComponent<ParticleSystem>().Play();
            Destroy(dashParticle, 1f);

            dashTriggered = true;
        }
    }

    public virtual void triggerHealParticle(){
        GameObject healParticle = Instantiate(healParticleContainer, transform);
        healParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        healParticle.GetComponent<ParticleSystem>().Play();
        Destroy(healParticle, 1f);
    }

    public virtual void triggerChargeAttackParticle(){
        GameObject caParticle = Instantiate(chargeAttackParticleContainer, transform);
        caParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        caParticle.GetComponent<ParticleSystem>().Play();
        Destroy(caParticle, 1f);
    }

    public virtual void triggerAirChargeAttackParticle(){
        GameObject jcaParticle = Instantiate(airChargeAttackParticleContainer, transform);
        jcaParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        jcaParticle.GetComponent<ParticleSystem>().Play();
        Destroy(jcaParticle, 1f);
    }

    public virtual void triggerAttack1Particle(){
        GameObject a1Particle = Instantiate(attack1ParticleContainer, transform);
        if(player.FacingDirection == 1) a1Particle.transform.position = new Vector3(transform.position.x+2.5f, transform.position.y, transform.position.z);
        else a1Particle.transform.position = new Vector3(transform.position.x-2.5f, transform.position.y, transform.position.z);
        a1Particle.GetComponent<ParticleSystem>().Play();
        Destroy(a1Particle, 1f);
    }

    public virtual void triggerAttack2Particle(){
        GameObject a2Particle = Instantiate(attack2ParticleContainer, transform);
        if(player.FacingDirection == 1) a2Particle.transform.position = new Vector3(transform.position.x+2.5f, transform.position.y, transform.position.z);
        else a2Particle.transform.position = new Vector3(transform.position.x-2.5f, transform.position.y, transform.position.z);
        a2Particle.GetComponent<ParticleSystem>().Play();
        Destroy(a2Particle, 1f);
    }

    public virtual void triggerAttack3Particle(){
        GameObject a3Particle = Instantiate(attack3ParticleContainer, transform);
        if(player.FacingDirection == 1) a3Particle.transform.position = new Vector3(transform.position.x+2f, transform.position.y, transform.position.z);
        else a3Particle.transform.position = new Vector3(transform.position.x-2f, transform.position.y, transform.position.z);
        a3Particle.GetComponent<ParticleSystem>().Play();
        Destroy(a3Particle, 1f);
    }

    public virtual void triggerUpAttackParticle(){
        GameObject uaParticle = Instantiate(upAttackParticleContainer, transform);
        uaParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        uaParticle.GetComponent<ParticleSystem>().Play();
        Destroy(uaParticle, 1f);
    }

    public virtual void triggerSideAirParticle(){
        GameObject saParticle = Instantiate(sideAirParticleContainer, transform);
        if(player.FacingDirection == 1) saParticle.transform.position = new Vector3(transform.position.x+2.5f, transform.position.y, transform.position.z);
        else saParticle.transform.position = new Vector3(transform.position.x-2.5f, transform.position.y, transform.position.z);
        saParticle.GetComponent<ParticleSystem>().Play();
        Destroy(saParticle, 1f);
    }

    public virtual void triggerDownAirParticle(){
        GameObject daParticle = Instantiate(downAirParticleContainer, transform);
        if(player.FacingDirection == 1) daParticle.transform.position = new Vector3(transform.position.x+.5f, transform.position.y, transform.position.z);
        else daParticle.transform.position = new Vector3(transform.position.x-.5f, transform.position.y, transform.position.z);
        daParticle.GetComponent<ParticleSystem>().Play();
        Destroy(daParticle, 1f);
    }

    public virtual void triggerFireballAnimParticle(){
        GameObject faParticle = Instantiate(fireballAnimParticleContainer, transform);
        if(player.FacingDirection == 1) faParticle.transform.position = new Vector3(transform.position.x+0.5f, transform.position.y, transform.position.z);
        else faParticle.transform.position = new Vector3(transform.position.x-0.5f, transform.position.y, transform.position.z);
        faParticle.GetComponent<ParticleSystem>().Play();
        Destroy(faParticle, 1f);
    }
}
