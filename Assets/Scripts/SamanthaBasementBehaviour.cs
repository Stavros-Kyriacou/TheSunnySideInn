using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamanthaBasementBehaviour : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform[] movementLocations;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartSequence()
    {
        gameObject.SetActive(true);
        animator.Play("Hallway_Scare");
    }
    public void PlayCrawlAnimation()
    {
        animator.Play("Crawl_Backwards");
    }


}
