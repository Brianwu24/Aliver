using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class KeybindLeft : MonoBehaviour
{
    public TextMeshProUGUI buttonLbl;

    private void Start() {
        buttonLbl.text = PlayerPrefs.GetString("CustomKey");
    }

    private void Update() {
        if (buttonLbl.text == "Awaiting Input") {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKey(keycode)) {
                    buttonLbl.text = keycode.ToString();
                    PlayerPrefs.SetString("CustomKey", keycode.ToString());
                    PlayerPrefs.Save();
                }
            }
        }
    }

    public void ChangeKey() {
        buttonLbl.text = "Awaiting Input";
    }
}