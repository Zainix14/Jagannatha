﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveGripExample_SwitchBulb : MonoBehaviour {
  bool on = false;

  void Start() {
    Toggle();
  }

  public void Toggle() {
    on = !on;
    Color newColor = on ? Color.black: Color.yellow;
    GetComponent<Renderer>().material.color = newColor;
  }
}
