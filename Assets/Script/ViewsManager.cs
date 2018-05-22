using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewsManager : MonoBehaviour
{
    public GameObject main;
    public GameObject decadesContainer;
    public GameObject categoriesContainer;
    public GameObject cylinder;
    public bool isMain;
    public bool isTransitioning = false;

    public List<GameObject> decades;
    public List<GameObject> categories;

    private GameObject currentView;
    private bool isDecade;
    private int numDecades;
    private int numCategories;
    private int currentDecadeIdx = 0;
    private int currentCategoryIdx = 0;

    // Use this for initialization
    void Start()
    {
        decades.Add(main);
        foreach(string decade in new[] { "50s", "60s", "70s", "80s", "90s", "00s", "10s", })
        {
            decades.Add(decadesContainer.transform.Find(decade).gameObject);
        }
        numDecades = decades.Count;

        categories.Add(main);
        foreach(string category in new[] { "Portraits of science", "Science and politics", "Inside the lab", "Outside the lab", "Machines", "Accelerators", "Experiments visualization", "Data processing", "Media", "CERN sceneries" })
        {
            categories.Add(categoriesContainer.transform.Find(category).gameObject);
        }

        numCategories = categories.Count;
        currentView = main;
        isMain = true;
        isDecade = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("CONTROLLER_RIGHT_TRIGGER"));
         
        if(Input.GetAxis("CONTROLLER_RIGHT_TRIGGER") > 0.7 && !isTransitioning && !isMain)
        {
            isDecade = false;
            isMain = true;
            currentCategoryIdx = 0;
            currentDecadeIdx = 0;
            Transition(currentView, main);
            currentView = main;
        }
        if ((Input.GetAxis("CONTROLLER_RIGHT_STICK_HORIZONTAL") > 0.7) && !isTransitioning && (isMain || isDecade))
        {
            if (currentDecadeIdx == numDecades - 1)
            {
                return;
            }
            isMain = false;
            isDecade = true;
            currentDecadeIdx++;
            Transition(currentView, decades[currentDecadeIdx]);
            currentView = decades[currentDecadeIdx];
        }

        if ((Input.GetAxis("CONTROLLER_RIGHT_STICK_HORIZONTAL") < -0.7) && !isTransitioning && (!isMain && isDecade))
        {
            currentDecadeIdx--;
            Transition(currentView, decades[currentDecadeIdx]);
            currentView = decades[currentDecadeIdx];

            isMain = currentDecadeIdx == 0;
            isDecade = !isMain;
        }

        if ((Input.GetAxis("CONTROLLER_RIGHT_STICK_VERTICAL") < -0.7) && !isTransitioning && (isMain || !isDecade))
        {
            if (currentCategoryIdx == numCategories - 1)
            {
                return;
            }
            isMain = false;
            isDecade = false;
            currentCategoryIdx++;
            Transition(currentView, categories[currentCategoryIdx]);
            currentView = categories[currentCategoryIdx];
        }

        if ((Input.GetAxis("CONTROLLER_RIGHT_STICK_VERTICAL") > 0.7) && !isTransitioning && (!isMain && !isDecade))
        {
            currentCategoryIdx--;
            Transition(currentView, categories[currentCategoryIdx]);
            currentView = categories[currentCategoryIdx];

            isMain = currentCategoryIdx == 0;
            isDecade = false;
        }


    }

    private void Transition(GameObject from, GameObject to)
    {
        cylinder.GetComponent<Animator>().SetBool("transition", true);
        from.GetComponent<Animator>().SetBool("Exit", true);
        StartCoroutine(DisableGameObject(from));
        to.SetActive(true);
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