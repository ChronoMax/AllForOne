using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;

    public Material[] materials;
    private Renderer rend;

    public string[] team;
    public int teamSelected = 0;
    public int[] currentMoney;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CreateTeams();
    }

    private void CreateTeams()
    {
        //Creates the team array and add 2 values in it.
        team = new string[2];
        team[0] = "Blue";
        team[1] = "Red";

        currentMoney = new int[2];
        currentMoney[0] = 100;
        currentMoney[1] = 100;
    }

    //Switching teams and assings them in other script that use it.
    public void TeamManager()
    {
        switch (teamSelected)
        {
            case 0:
                teamSelected = 1;
                break;
            case 1:
                teamSelected = 0;
                break;
            default:
                Debug.LogError("Team cannot be assinged");
                break;
        }
    }

    //Will handle the spawning of the units.
    //Adds the color based of the team that is selected.
    //Spawns with the correct values that has been set by the sliders.
    //Adds an tag for the current team that is spawning the units.
    public void Spawn(Transform transformToSpawn)
    {
        GameObject unitToSpawn = UnitConfig.Instance.unitToSpawn;
        Slider[] sliders = UnitConfig.Instance.sliders;

        GameObject unit = Instantiate(unitToSpawn, transformToSpawn.position, Quaternion.identity);

        rend = unit.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[teamSelected];

        unit.GetComponent<Renderer>();

        unit.GetComponent<Unit>().SpawnWithValues(((int)sliders[0].value), ((int)sliders[1].value), ((int)sliders[2].value), ((int)sliders[3].value));
        unit.tag = team[teamSelected];
    }
}