using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouseDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [Header("LeftDrag")]
    [SerializeField] RectTransform LeftRectTransform;
    Vector2 LeftMenuStartPos;
    float LeftMenuX;
    bool leftDrag = false;

    [Header("RightDrag")]
    [SerializeField] RectTransform RightRectTransform;
    Vector2 RightMenuStartPos;
    float RightMenuX;
    bool rightDrag = false;

    Vector2 startPos;
    [SerializeField] RectTransform BackGroundrectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] bool LastLeft = false;
    [SerializeField] bool LastRight = false;


    [SerializeField] float minimunDrag = 200f;

    private void Awake()
    {
        if (LeftRectTransform != null) LeftMenuStartPos = LeftRectTransform.anchoredPosition;
        if (RightRectTransform != null) RightMenuStartPos = RightRectTransform.anchoredPosition;
        if (LastLeft || LastRight) { startPos = transform.GetComponent<RectTransform>().anchoredPosition; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!rightDrag)
        {

            if ((transform.GetComponent<RectTransform>().anchoredPosition.x) > (transform.GetComponent<RectTransform>().anchoredPosition.x + eventData.delta.x))
            {
                leftDrag = true;
            }
        }
        if (!leftDrag)
        {
            if ((transform.GetComponent<RectTransform>().anchoredPosition.x) < (transform.GetComponent<RectTransform>().anchoredPosition.x + eventData.delta.x))
            {
                rightDrag = true;

            }
        }


        if (leftDrag && !rightDrag)
        {
            if (LeftRectTransform != null) LeftRectTransform.anchoredPosition = LeftRectTransform.anchoredPosition + (new Vector2(eventData.delta.x, 0f) / canvas.scaleFactor);
        }
        if (!leftDrag && rightDrag)
        {
            if (RightRectTransform != null) RightRectTransform.anchoredPosition = RightRectTransform.anchoredPosition + (new Vector2(eventData.delta.x, 0f) / canvas.scaleFactor);
        }


        if (leftDrag && LastLeft)
        {
            transform.GetComponent<RectTransform>().anchoredPosition += new Vector2(eventData.delta.x, 0f) / canvas.scaleFactor;

        }
        if (rightDrag && LastRight)
        {
            transform.GetComponent<RectTransform>().anchoredPosition += new Vector2(eventData.delta.x, 0f) / canvas.scaleFactor;

        }


    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (leftDrag)
        {
            if (LeftRectTransform != null)
                if (LeftMenuX - minimunDrag > LeftRectTransform.anchoredPosition.x)
                {
                    LeftRectTransform.anchoredPosition = BackGroundrectTransform.anchoredPosition;
                }
                else
                {
                    LeftRectTransform.anchoredPosition = LeftMenuStartPos;
                }
        }

        if (rightDrag)
        {
            if (RightRectTransform != null)
                if (RightMenuX + minimunDrag < RightRectTransform.anchoredPosition.x)
                {
                    RightRectTransform.anchoredPosition = BackGroundrectTransform.anchoredPosition;
                }
                else
                {
                    RightRectTransform.anchoredPosition = RightMenuStartPos;
                }
        }
		
		if (leftDrag)
        {
            if (LastLeft){transform.GetComponent<RectTransform>().anchoredPosition=startPos;}                             
        }
		if (rightDrag)
        {
            if (LastRight){transform.GetComponent<RectTransform>().anchoredPosition=startPos;}                             
        }
		

		
        leftDrag = false;
        rightDrag = false;



    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (LeftRectTransform != null) LeftMenuX = LeftRectTransform.anchoredPosition.x;
        if (RightRectTransform != null) RightMenuX = RightRectTransform.anchoredPosition.x;
    }




}
