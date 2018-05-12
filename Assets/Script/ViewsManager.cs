using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewsManager : MonoBehaviour
{
    public GameObject overview;
    public GameObject categories;
    public GameObject cylinder;
    public bool isMain = true;
    public bool isTransitioning = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning)
        {
            cylinder.GetComponent<Animator>().SetBool("transition", true);
            if (isMain)
            {
                overview.GetComponent<Animator>().SetBool("Exit", true);


                StartCoroutine(DisableGameObject(overview));
                categories.SetActive(true);
            } else
            {
                categories.GetComponent<Animator>().SetBool("Exit", true);
                StartCoroutine(DisableGameObject(categories));
                overview.SetActive(true);
            }
            isMain = !isMain;
        }
    }

    private IEnumerator DisableGameObject(GameObject toDisable)
    {
        isTransitioning = true;
        yield return new WaitForSeconds(1);
        toDisable.SetActive(false);
        isTransitioning = false;
        cylinder.GetComponent<Animator>().SetBool("transition", false);
    }
}