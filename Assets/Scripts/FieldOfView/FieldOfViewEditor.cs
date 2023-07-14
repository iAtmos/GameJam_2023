//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(AIFieldOfView))]
//public class FieldOfViewEditor : Editor
//{
//    private void OnSceneGUI()
//    {
//        var fov = (AIFieldOfView)target;
//        Handles.color = Color.white;
//        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.RadiusFieldsView);

//        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.AngleView / 2);
//        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.AngleView / 2);

//        Handles.color = Color.yellow;
//        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.RadiusFieldsView);
//        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.RadiusFieldsView);

//        if (fov.canSeePlayer)
//        {
//            Handles.color = Color.green;
//            //Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
//        }
//    }

//    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
//    {
//        angleInDegrees += eulerY;

//        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
//    }
//}