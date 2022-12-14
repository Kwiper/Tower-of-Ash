using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicationManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        checker();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator duplicationChecker()
    {

        yield return new WaitForSecondsRealtime(1.5f);
        var essentialObjectList = FindObjectsOfType<EssentialObjects>();
        if(essentialObjectList.Length > 1){
            for (int i = 0; i < essentialObjectList.Length; i++) 
            {
                if(essentialObjectList[i].GetComponentInChildren<Player>().isReal == false){
                    Destroy(essentialObjectList[i]);
                }
            }
        }
        
    }

    private void checker(){
        StartCoroutine(duplicationChecker());
    }
}
