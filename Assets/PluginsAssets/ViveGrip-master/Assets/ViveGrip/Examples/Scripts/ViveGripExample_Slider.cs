using UnityEngine;
using System.Collections;

public class ViveGripExample_Slider : MonoBehaviour {
  private ViveGrip_ControllerHandler controller;
  private float oldX;
  private int VIBRATION_DURATION_IN_MILLISECONDS = 50;
  private float MAX_VIBRATION_STRENGTH = 0.2f;
  private float MAX_VIBRATION_DISTANCE = 0.03f;


    private float _localX = 0;
    private float _localY = 0;
    private float _localZ = 0;
    public bool _freezeAlongX = true;
    public bool _freezeAlongY = false;
    public bool _freezeAlongZ = true;

    private Rigidbody sliderRigidbody;

    void Start () {
    oldX = transform.position.x;

        sliderRigidbody = gameObject.GetComponent<Rigidbody>();
  }

  void ViveGripGrabStart(ViveGrip_GripPoint gripPoint) {
    controller = gripPoint.controller;
        sliderRigidbody.isKinematic = false;
  }

  void ViveGripGrabStop() {
    controller = null;
        sliderRigidbody.isKinematic = true;
    }

	void Update () {


        _localX = transform.localPosition.x;
        _localY = transform.localPosition.y;
        _localZ = transform.localPosition.z;

        if (_freezeAlongX) _localX = 0;
        if (_freezeAlongY) _localY = 0;
        if (_freezeAlongZ) _localZ = 0;
        gameObject.transform.localPosition = new Vector3(_localX, _localY, _localZ);
        Debug.Log(gameObject.transform.localPosition.y);


        float newX = transform.position.x;
    if (controller != null) {
      float distance = Mathf.Min(Mathf.Abs(newX - oldX), MAX_VIBRATION_DISTANCE);
      float vibrationStrength = (distance / MAX_VIBRATION_DISTANCE) * MAX_VIBRATION_STRENGTH;
      controller.Vibrate(VIBRATION_DURATION_IN_MILLISECONDS, vibrationStrength);
    }
    oldX = newX;
	}
}
