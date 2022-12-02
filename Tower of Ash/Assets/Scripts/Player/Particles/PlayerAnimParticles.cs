using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimParticles : MonoBehaviour
{
    public Animator animator;

    #region Particles
    [SerializeField] GameObject moveStartParticleContainer;
    [SerializeField] GameObject moveParticleContainer;
    #endregion

    #region Misc
    private bool moveStartTriggered = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("MoveStart")) moveStartTriggered = false;
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
}
