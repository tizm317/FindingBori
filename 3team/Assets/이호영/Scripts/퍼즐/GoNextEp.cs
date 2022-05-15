using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoNextEp : MonoBehaviour
{
    private Camera _camera;
    public GameObject textPanel;

    [SerializeField] private GameObject last_BG; // illerstrate bg

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit)
                {
                    if (hit.collider.gameObject.name == "TouchArea")
                    {
                        //textPanel.SetActive(true);
                        this.gameObject.transform.parent.gameObject.SetActive(false);
                        loadNextScene();

                    }
                }
            }
               
        }
    }

    void loadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int curScene = scene.buildIndex;
        int nextScene = curScene + 1;

        if (curScene != 4)
            SceneManager.LoadScene(nextScene);
        else
        {
            textPanel.SetActive(true);
            last_BG.SetActive(true);
        }
    }
}
