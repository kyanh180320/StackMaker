using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject brick;
    private bool isUnBrick = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_PLAYER) && isUnBrick)
        {
            isUnBrick= false;
            if(!other.GetComponent<PlayerParent>().CheckListBircks())
            {
                return;
            }
            brick.SetActive(true);
            other.GetComponent<PlayerParent>().RemoveBrick();

        }
    }
}
