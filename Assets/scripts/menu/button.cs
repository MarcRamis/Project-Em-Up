using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public Camera cam;

    public GameObject cursor;

    public bool goMenu = false;
    public bool exit = false;
    public bool options = false;
    public bool press = false;
    public bool mousePosition = false;
    public Vector3 vectorMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mousePosition)
        {
            vectorMousePosition = cam.WorldToScreenPoint(Input.mousePosition);
            cursor.transform.position = new Vector3(vectorMousePosition.x / 6000, vectorMousePosition.y / 6000, 0);
        }
            

        if (press && goMenu && Input.GetKeyDown(KeyCode.Mouse0))
            SceneManager.LoadScene("menu");

       //else if (press && options)
       //{
       //
       //}

        else if (press && exit && Input.GetKeyDown(KeyCode.Mouse0))
            Application.Quit();
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag.Equals("mouse"))
            press = true;
    }            

    void OnTriggerExit(Collider Other)
    {
        if (Other.tag.Equals("mouse"))
            press = false;
    }    
}
