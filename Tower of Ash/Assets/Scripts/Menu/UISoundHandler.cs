using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundHandler : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip menuMove;
    public AudioClip menuConfirm;
    public AudioClip menuIncorrect;
    public AudioClip ascendButton;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMoveMenuSound()
    {
        audioSource.PlayOneShot(menuMove);
    }

    public void PlayMenuConfirmSound()
    {
        audioSource.PlayOneShot(menuConfirm);
    }

    public void PlayMenuIncorrectSound()
    {
        audioSource.PlayOneShot(menuIncorrect);
    }

    public void PlayAscendSound()
    {
        audioSource.PlayOneShot(ascendButton);
    }
}
