using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.Salles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{

    // Map of the room
    private Carte carte;

    // the current selected playable by the player
    private GameObject currentSelectedPlayable;
    public GameObject CurrentSelectedPlayable { get => currentSelectedPlayable; set => currentSelectedPlayable = value; }


    [SerializeField]
    private GameObject playablePreFab;



    [SerializeField]
    private GameObject selectionTileManager;

    // List of thep playable in the room
    private List<GameObject> playables = new List<GameObject>();

    


    /// <summary>
    /// Create the playables on the room
    /// </summary>
    /// <param name="carte">the room</param>
    public void CreatePlayableCharacters(Carte carte)
    {

        DeletePlayable();
        selectionTileManager.GetComponent<SelectionTileManager>().DeleteSelectionTiles();

        this.carte = carte;

        for (int i = 0; i < 3; i++)
        {
            int ligne = GenerateurAleatoire.Instance.Next(3) + 1;
            int colonne = GenerateurAleatoire.Instance.Next(3) + 1;


            // If we can't put the playable, we retry with another coordinates
            while (!PlayableCanBePlaced(ligne,colonne))
            {
                ligne = GenerateurAleatoire.Instance.Next(Carte.Taille - 1) + 1;
                colonne = GenerateurAleatoire.Instance.Next(3) + 1;
            }


            carte.Salles[ligne, colonne].HasPlayer = true;
            GameObject playable = Instantiate(playablePreFab);
            playable.transform.position = new Vector3(GameManager.roomPosition + colonne, GameManager.roomPosition + ligne, - 1);
            playables.Add(playable);


            selectionTileManager.GetComponent<SelectionTileManager>().Carte = carte;
        }
    }

    /// <summary>
    /// Verify if a playable can be place in a room
    /// </summary>
    /// <returns>the boolean of the response</returns>
    private bool PlayableCanBePlaced(int ligne, int colonne)
    {
        bool result = true;

        if (carte.Salles[ligne,colonne].Type == TypeSalle.TILEFULL || carte.Salles[ligne, colonne].HasPlayer)
        {
            result = false;
        }

        return result;
    }

    /// <summary>
    /// Delete all the playable on the terrain
    /// </summary>
    public void DeletePlayable()
    {
        for (int i = 0; i < playables.Count; i++)
        {
            Destroy(playables[i]);
        }
        playables.Clear();
    }

    /// <summary>
    /// Move the player on the path that has been given
    /// </summary>
    /// <param name="path"></param>
    public void MoveCurrentPlayer(List<GameObject> path)
    {

    }

}
