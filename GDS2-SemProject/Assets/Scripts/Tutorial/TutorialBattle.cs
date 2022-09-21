using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBattle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BattleNode enemyCastle;
    [SerializeField] private GameObject popup;
    private bool enabled;
    void Start()
    {
        enemyCastle = gameObject.GetComponent<BattleNode>();
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyCastle.IsEnemy() && !enabled)
        {
            popup.SetActive(true);
            enabled = true;
        }
    }
}
