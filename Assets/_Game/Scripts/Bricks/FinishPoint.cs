using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            ball.SetActive(false);
            GameManager.Instance.WinGame();

        }
    }
}
