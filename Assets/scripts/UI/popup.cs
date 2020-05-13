using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
    void Start()
    {
        // Destruim el ¡Begin! del inici perquè sino ha desaparegut en el temps suficient 
        // es veu per damunt del popup 
        if (GameObject.Find("Level 1 anim") != null)
        {
            Destroy(GameObject.Find("Level 1 anim"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Time.timeScale = 1;
            Destroy(this.gameObject);
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
