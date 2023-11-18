using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBrick : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject brick;
    private bool isEatBrick = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_PLAYER) && isEatBrick)
        {
            DataManager.Instance.IncreaseScore();
            isEatBrick = false;
            brick.SetActive(false);
            other.GetComponent<PlayerParent>().AddBrick();
        }
    }
 
}
