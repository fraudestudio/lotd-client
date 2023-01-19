using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Objets;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour, IGeneratorAlgo
{
    [SerializeField]
    // The gameobject of the boss room
    private GameObject salleBoss;
    [SerializeField]
    // The gameobject of a normal room
    private GameObject salleNormale;
    [SerializeField]
    // the gameobject of an empty room
    private GameObject salleVide;
    [SerializeField]
    // the gameobject of a start room
    private GameObject salleStart;
    [SerializeField]
    // the gameobject of the map camera
    private Camera camera;
    [SerializeField]
    // the gameobject of the indication arrow
    private GameObject arrow;

    // the map 
    private Carte carte;

    // the gameobject of the first room 
    private GameObject firstRoom;
    public GameObject FirstRoom { get => firstRoom; }

    // the array of the room 
    // by line and column 
    private GameObject[,] salles;

    /// <summary>
    /// Generate the map 
    /// </summary>
    /// <param name="carte">the map we want to generate</param>
    public void GenerateMap(Carte carte)
    {
        // Size of the map
        Carte.Taille = 6;

        // We create the list of room 
        salles = new GameObject[Carte.Taille, Carte.Taille];

        // we save the mapo 
        this.carte = carte;

        // we create the bounds for the camera
        Bounds mapBounds = new Bounds();

        // we create all the room
        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                if (carte.Salles[ligne, colonne].Ligne == ligne && carte.Salles[ligne, colonne].Colonne == colonne)
                {
                    // we create the gameobject
                    GameObject salle = GetObjectSalle(carte.Salles[ligne, colonne].Type);

                    // we rename it
                    salle.name = "Salle_" + ligne + "_" + colonne + "_" + carte.Salles[ligne, colonne].Type.ToString();

                    // we position it 
                    salle.transform.position = new Vector2(colonne, ligne);
                    
                    // We save the room and the seed
                    salle.GetComponent<SalleObjectScript>().Salle = carte.Salles[ligne, colonne];
                    salle.GetComponent<SalleObjectScript>().Seed = int.Parse(ligne.ToString() + colonne.ToString());

                    // We add the room on the array 
                    salles[ligne,colonne] = salle;

                    // if it is the start room, we save it as the first room and move the arrow on it
                    if (carte.Salles[ligne, colonne].Type == TypeSalle.START)
                    {
                        firstRoom = salle;
                        arrow.transform.position = new Vector3(colonne, ligne, -8);
                    }

                    // we encapsulate the map for the camera to focus 
                    if (carte.Salles[ligne, colonne].Type != TypeSalle.VIDE)
                    {
                        mapBounds.Encapsulate(salle.transform.position);
                    }

                }
            }
        }

        // We set the neigboors
        SetVoisinsInObjects();

        // We set the arrow as last objecgts 
        arrow.transform.SetAsLastSibling();
        // We move the camera to the center
        camera.transform.position = new Vector3(mapBounds.center.x, mapBounds.center.y,-10);
        camera.orthographicSize = mapBounds.size.x / 1.2f;
    }

    /// <summary>
    /// Set the neigboor for all the gameobjects 
    /// </summary>
    private void SetVoisinsInObjects()
    {
        for (int ligne = 0; ligne < Carte.Taille; ligne++)
        {
            for (int colonne = 0; colonne < Carte.Taille; colonne++)
            {
                GameObject salle = salles[ligne,colonne];


                if (ligne + 1 < Carte.Taille)
                {
                    if (carte.Salles[ligne + 1, colonne].Type != TypeSalle.VIDE)
                    {
                        salle.GetComponent<SalleObjectScript>().Top = salles[ligne + 1, colonne];
                    }
                }

                if (ligne - 1 >= 0)
                {
                    if (carte.Salles[ligne - 1, colonne].Type != TypeSalle.VIDE)
                    {
                        salle.GetComponent<SalleObjectScript>().Bottom = salles[ligne - 1, colonne];
                    }
                }

                if (colonne + 1 < Carte.Taille)
                {
                    if (carte.Salles[ligne, colonne + 1].Type != TypeSalle.VIDE)
                    {
                        salle.GetComponent<SalleObjectScript>().Right = salles[ligne, colonne + 1];
                    }
                }

                if (colonne - 1 >= 0)
                {
                    if (carte.Salles[ligne, colonne - 1].Type != TypeSalle.VIDE)
                    {
                        salle.GetComponent<SalleObjectScript>().Left = salles[ligne, colonne - 1];
                    }
                }

            }
        }
    }

    /// <summary>
    /// Create the gameobject of the given type
    /// </summary>
    /// <param name="salle">the type of the room</param>
    /// <returns>the created objects</returns>
    public GameObject GetObjectSalle(TypeSalle salle)
    {
        GameObject result;

        switch (salle)
        {
            case TypeSalle.NORMALE: result = Instantiate(salleNormale); break;
            case TypeSalle.START: result = Instantiate(salleStart); break;
            case TypeSalle.BOSS: result = Instantiate(salleBoss); break;
            default: result = Instantiate(salleVide); break;
        }

        return result;
    }

    /// <summary>
    /// Set the arrow on the right position 
    /// </summary>
    /// <param name="ligne">line position of the arrow</param>
    /// <param name="colonne">column position of the arrow</param>
    public void SetArrowPosition(int ligne, int colonne)
    {
        arrow.transform.position = new Vector3(colonne, ligne, -8);
    }
}
