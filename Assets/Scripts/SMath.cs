using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class SMath
{
    public static Single ReturnAngleFromPositions(Vector3 startingPosition, Vector3 endPosition)
    {
        Single deltaX = endPosition.x - startingPosition.x;
        Single deltaY = endPosition.y - startingPosition.y;
        Double radResult = Math.Atan2((Double)deltaY, (Double)deltaX);
        Single degResult = (Single)(radResult * (180 / Math.PI));
        return degResult + 180;
    }
}
