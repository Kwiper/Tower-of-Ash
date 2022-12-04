using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimParticles : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Player player;

    #region Particles
    [SerializeField] GameObject moveStartParticleContainer;
    [SerializeField] GameObject moveParticleContainer;
    [SerializeField] GameObject wallSlideParticleContainer;
    #endregion

    #region Misc
    private bool moveStartTriggered = false;
    private bool jumpUpTriggered = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("MoveStart")) moveStartTriggered = false;
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("JumpUp1")) jumpUpTriggered = false;
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
}
