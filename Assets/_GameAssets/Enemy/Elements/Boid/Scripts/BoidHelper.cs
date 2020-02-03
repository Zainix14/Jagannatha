using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calcul de la direction optimale selon raycast sphérique
/// </summary>
public static class BoidHelper {

    const int numViewDirections = 10; //Nombre de Directions 
    public static readonly Vector3[] directions; //Tableau contenant les directions

    static BoidHelper ()
    {
        
        directions = new Vector3[BoidHelper.numViewDirections]; //Remplissage du tableau avec le nombre d'éléments

        float goldenRatio = (1 + Mathf.Sqrt (5)) / 2; //Nombre d'or (env 1.61)
        float angleIncrement = Mathf.PI * 2 * goldenRatio; //Décalage d'angle

        for (int i = 0; i < numViewDirections; i++)
        {
            float t = (float) i / numViewDirections; //Interval entre chaque direction
            float inclination = Mathf.Acos (1 - 2 * t); //Angle entre chaque direction
            float azimuth = angleIncrement * i; //Angle sur le plan horizontal entre direction d'un objet et direction de ref (wiki)

            float x = Mathf.Sin (inclination) * Mathf.Cos (azimuth); //Direction en X
            float y = Mathf.Sin (inclination) * Mathf.Sin (azimuth); //Directon en Y
            float z = Mathf.Cos (inclination); //Direction en Z
            directions[i] = new Vector3 (x, y, z); //Stocakge dans le vector3
        }
    }

}