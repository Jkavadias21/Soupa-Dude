using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintPopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Hint;

    private void Start() {
        Hint.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Hint.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Hint.enabled = false;
    }


}
