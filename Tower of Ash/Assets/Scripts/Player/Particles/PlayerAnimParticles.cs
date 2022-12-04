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
    #endregion

    #region Misc Particles
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
}
