﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; //for lists


public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }




    


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.B)) BattleManager.instance.StartNewBattle(DataManager.DM.playerSquads[0], DataManager.DM.enemySquads[0]);
        if(Input.GetKeyDown(KeyCode.S))
            DataManager.DM.Save();
        if(Input.GetKeyDown(KeyCode.L))
            DataManager.DM.Load();
        if(Input.GetKeyDown(KeyCode.R))
            RollAllCharacters(1);

    }

    public void RollAllCharacters(int level)
    {
        foreach(CharacterSheet sheet in DataManager.DM.playerSheets)
        {
            RollNewCharacter(sheet,level);
        }
        foreach(CharacterSheet sheet in DataManager.DM.enemySheets)
        {
            RollNewCharacter(sheet, level);
        }


    }

    public void RollNewCharacter(CharacterSheet sheet, int level)
    {
        //foreach(OddsToStat row in DataManager.DM.characterClassDictionary[sheet.characterClass].levelUpStatOdds)
        //{
        //    Debug.Log(sheet.characterClass.ToString() +" "+row.stat.ToString() + " " + row.range.x.ToString() + " " + row.range.y.ToString());
        //}

        sheet.stats = new CharacterStats();
        sheet.level = 1- GameSettings.LevelOneStartingStats_NumberOfLevelsFromZeros;
        sheet.elementalType = (ElementalType)Random.Range(1, 5);

        for (int i =0; i< GameSettings.LevelOneStartingStats_NumberOfLevelsFromZeros + level - 1; i++)
        {
            LevelUp(sheet);
        }
    }

    public void LevelUp(CharacterSheet sheet)
    {
        sheet.level += 1;
        sheet.stats.hitPoints += Random.Range(GameSettings.hitPointsMinOnLevelUp, GameSettings.hitPointsMaxOnLevelUp+1);

        for(int i = 0; i < GameSettings.statPointsOnLevelUp; i++)
        {
            float roll = RollZeroToUnderOne();
            foreach(OddsToStat row in DataManager.DM.characterClassDictionary[sheet.characterClass].levelUpStatOdds)
            {
                if(roll >= row.range.x && roll < row.range.y)
                {
                    switch(row.stat)
                    {
                        case StatType.STRENGTH:
                            {
                                sheet.stats.strength += 1;
                                break;
                            }
                        case StatType.TOUGHNESS:
                            {
                                sheet.stats.toughness += 1;
                                sheet.stats.hitPoints += 1;
                                break;
                            }
                        case StatType.AGILITY:
                            {
                                sheet.stats.agility += 1;
                                break;
                            }
                        case StatType.MAGIC:
                            {
                                sheet.stats.magic += 1;
                                break;
                            }
                        case StatType.SPIRIT:
                            {
                                sheet.stats.spirit += 1;
                                sheet.stats.hitPoints += 1;
                                break;
                            }
                        case StatType.MIND:
                            {
                                sheet.stats.mind += 1;
                                break;
                            }
                        case StatType.CHARISMA:
                            {
                                sheet.stats.charisma += 1;
                                break;
                            }
                        case StatType.RESOLVE:
                            {
                                sheet.stats.resolve += 1;
                                sheet.stats.hitPoints += 1;
                                break;
                            }
                        case StatType.SPEED:
                            {
                                sheet.stats.speed += 1;
                                break;
                            }
                        case StatType.LUCK:
                            {
                                sheet.stats.luck += 1;
                                break;
                            }
                        default:
                            break;
                    }//end switch to add point
                }//end if roll in range
            }//end foreach row in odds table
        }//end each stat point
    }//end function

    public float RollZeroToUnderOne()
    {
        return Random.Range(0, GameSettings.randomNumberResolution) / ((float)GameSettings.randomNumberResolution);
    }

}
