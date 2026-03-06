using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectivelyImpassableArea : MonoBehaviour
{
    [SerializeField]
    List<string> tagsToLetThrough;
    private void OnCollisionEnter(Collision collision)
    {
        if (CheckPass(collision.gameObject))
        {
            Debug.Log($"{collision.gameObject.name} is added to the list of physics exceptions  ");
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    protected bool CheckPass(GameObject gameObject)
    {
        foreach (string tag in tagsToLetThrough)
        {
            if (gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
