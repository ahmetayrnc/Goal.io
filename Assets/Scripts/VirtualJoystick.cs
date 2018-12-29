using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

    public Image bgImg;
    public Image joystickImg;

    Vector3 inputVector;

    Vector2 start;

    private void Start()
    {
        //bgImg = GetComponent<Image>();
        //joystickImg = transform.GetChild(0).GetComponent<Image>();
        //bgImg.enabled = false;
        //joystickImg.enabled = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform
                                                                   ,eventData.position
                                                                   ,eventData.pressEventCamera
                                                                   ,out pos))
        {
            //Debug.Log(pos);
            
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //bgImg.transform.Translate(pos*5);

            joystickImg.rectTransform.anchoredPosition = new Vector3(
                                inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3)
                                , inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3));

            //Debug.Log(inputVector);
        }

        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //bgImg.enabled = true;
        //joystickImg.enabled = true;
        bgImg.transform.position = eventData.position;
        OnDrag(eventData);
        //Debug.Log(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;

        //bgImg.enabled = false;
        //joystickImg.enabled = false;
    }


    public float Horizontal()
    {
        if (!inputVector.x.Equals(0f))
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");  
    }

    public float Vertical()
    {
        if (!inputVector.z.Equals(0f))
       // if (inputVector.z != 0f)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}
