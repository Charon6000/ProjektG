using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] bool toDynamic;
    [SerializeField] CinemachineVirtualCameraBase[] cams;
    [SerializeField] Transform Player;
    [SerializeField] Transform WhereToResp;
    [SerializeField] Animator[] anim = new Animator[2];

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        anim[0] = cams[0].gameObject.GetComponent<Animator>();
        anim[1] = cams[1].gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.TryGetComponent(out OWController controller))
        {
            StartCoroutine(fade());
            StartCoroutine(controller.stopMove());
        }
        if(toDynamic){
            cams[1].Follow = Player; 
        }
    }
    IEnumerator fade()
    {
        anim[0].SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        anim[1].SetTrigger("FadeIn");
        cams[0].Priority = 0;
        cams[1].Priority = 1;
        Player.position = WhereToResp.position;
    }
}
