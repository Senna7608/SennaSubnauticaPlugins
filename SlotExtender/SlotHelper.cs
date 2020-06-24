﻿using System;
using System.Collections.Generic;
using SlotExtender.Configuration;
using Common;
using UnityEngine;

namespace SlotExtender
{
    public enum NewEquipmentType
    {
        SeamothArm = 100
    };

    public struct SlotData
    {
        public string SlotID;
        public int SlotNUM;
        public Vector2 SlotPOS;

        public SlotData(string slotID, int slotNUM, Vector2 slotPOS)
        {
            SlotID = slotID;
            SlotNUM = slotNUM;
            SlotPOS = slotPOS;
        }
    }


    internal static class SlotHelper
    {
        //private static bool SlotMappingExpanded = false;

        internal static string[] SessionSeamothSlotIDs { get; private set; }
        internal static string[] SessionExosuitSlotIDs { get; private set; }

        private const float Unit = 200f;
        private const float RowStep = Unit * 2.2f / 3;
        private const float TopRow = Unit;
        private const float SecondRow = TopRow - RowStep;
        private const float ThirdRow = SecondRow - RowStep;
        private const float FourthRow = ThirdRow - RowStep;
        private const float FifthRow = FourthRow - RowStep;
        private const float CenterColumn = 0f;
        private const float RightColumn = RowStep;
        private const float LeftColumn = -RowStep;
        
        private static readonly Vector2[] slotPos = new Vector2[12]
        {
            new Vector2(LeftColumn, TopRow), //slot 1
            new Vector2(CenterColumn, TopRow),  //slot 2
            new Vector2(RightColumn, TopRow),   //slot 3

            new Vector2(LeftColumn, SecondRow),  //slot 4
            new Vector2(CenterColumn, SecondRow), //slot 5
            new Vector2(RightColumn, SecondRow),   //slot 6

            new Vector2(LeftColumn, ThirdRow),  //slot 7
            new Vector2(CenterColumn, ThirdRow),  //slot 8
            new Vector2(RightColumn, ThirdRow),   //slot 9

            new Vector2(LeftColumn, FourthRow),   //slot 10
            new Vector2(CenterColumn, FourthRow),  //slot 11
            new Vector2(RightColumn, FourthRow)  //slot 12
        };

        private static readonly Vector2[] armSlotPos = new Vector2[2]
        {
            new Vector2(LeftColumn, FifthRow), //arm slot left
            new Vector2(RightColumn, FifthRow) //arm slot right
        };

        internal static readonly string[] ExpandedSeamothSlotIDs = new string[14]
        {
            "SeamothModule1",
            "SeamothModule2",
            "SeamothModule3",
            "SeamothModule4",
            // New slots start here            
            "SeamothModule5",
            "SeamothModule6",
            "SeamothModule7",
            "SeamothModule8",
            "SeamothModule9",
            "SeamothModule10",
            "SeamothModule11",
            "SeamothModule12",
            "SeamothArmLeft",
            "SeamothArmRight"
        };

        public static readonly List<SlotData> BaseSeamothSlotsData = new List<SlotData>()
        {
            new SlotData("SeamothModule1", 1, slotPos[0]),
            new SlotData("SeamothModule2", 2, slotPos[1]),
            new SlotData("SeamothModule3", 3, slotPos[2]),
            new SlotData("SeamothModule4", 4, slotPos[3])
        };

        public static readonly List<SlotData> NewSeamothSlotsData = new List<SlotData>()
        {
            new SlotData("SeamothModule5", 5, slotPos[4]),
            new SlotData("SeamothModule6", 6, slotPos[5]),
            new SlotData("SeamothModule7", 7, slotPos[6]),
            new SlotData("SeamothModule8", 8, slotPos[7]),
            new SlotData("SeamothModule9", 9, slotPos[8]),
            new SlotData("SeamothModule10", 10, slotPos[9]),
            new SlotData("SeamothModule11", 11, slotPos[10]),
            new SlotData("SeamothModule12", 12, slotPos[11])
        };

        public static readonly List<SlotData> NewSeamothArmSlotsData = new List<SlotData>()
        {
            new SlotData("SeamothArmLeft", 13, armSlotPos[0]),
            new SlotData("SeamothArmRight", 14, armSlotPos[1])
        };


        internal static readonly string[] ExpandedExosuitSlotIDs = new string[14]
        {
            "ExosuitArmLeft",
            "ExosuitArmRight",
            "ExosuitModule1",
            "ExosuitModule2",
            "ExosuitModule3",
            "ExosuitModule4",
            // New slots start here
            "ExosuitModule5",
            "ExosuitModule6",
            "ExosuitModule7",
            "ExosuitModule8",
            "ExosuitModule9",
            "ExosuitModule10",
            "ExosuitModule11",
            "ExosuitModule12"
        };

        public static readonly List<SlotData> BaseExosuitSlotsData = new List<SlotData>()
        {
            new SlotData("ExosuitModule1", 1, slotPos[0]),
            new SlotData("ExosuitModule2", 2, slotPos[1]),
            new SlotData("ExosuitModule3", 3, slotPos[2]),
            new SlotData("ExosuitModule4", 4, slotPos[3])
        };

        public static readonly List<SlotData> NewExosuitSlotsData = new List<SlotData>()
        {
            new SlotData("ExosuitModule5", 5, slotPos[4]),
            new SlotData("ExosuitModule6", 6, slotPos[5]),
            new SlotData("ExosuitModule7", 7, slotPos[6]),
            new SlotData("ExosuitModule8", 8, slotPos[7]),
            new SlotData("ExosuitModule9", 9, slotPos[8]),
            new SlotData("ExosuitModule10", 10, slotPos[9]),
            new SlotData("ExosuitModule11", 11, slotPos[10]),
            new SlotData("ExosuitModule12", 12, slotPos[11])
        };





        internal static IEnumerable<string> NewSeamothSlotIDs
        {
            get
            {
                foreach (SlotData slotData in NewSeamothSlotsData)
                {
                    if (slotData.SlotNUM <= SEConfig.MAXSLOTS)
                    {
                        yield return slotData.SlotID;
                    }
                }

                foreach (SlotData slotData in NewSeamothArmSlotsData)
                {
                    yield return slotData.SlotID;
                }
                /*
                for (int i = 4; i < SEConfig.MAXSLOTS + 2; i++)
                {
                    if (i == SEConfig.MAXSLOTS)
                    {
                        yield return ExpandedSeamothSlotIDs[12];
                    }
                    else if (i == SEConfig.MAXSLOTS + 1)
                    {
                        yield return ExpandedSeamothSlotIDs[13];
                    }
                    else
                    {
                        yield return ExpandedSeamothSlotIDs[i];
                    }
                }
                */
            }
        }        

        internal static IEnumerable<string> NewExosuitSlotIDs
        {
            get
            {
                foreach (SlotData slotData in NewExosuitSlotsData)
                {
                    if (slotData.SlotNUM <= SEConfig.MAXSLOTS)
                    {
                        yield return slotData.SlotID;
                    }
                }

                /*
                for (int i = 6; i < SEConfig.MAXSLOTS + 2; i++)
                {
                    yield return ExpandedExosuitSlotIDs[i];
                }
                */
            }
        }
        
        internal static void InitSlotIDs()
        {
            SessionSeamothSlotIDs = (string[])Array.CreateInstance(typeof(string), SEConfig.MAXSLOTS + 2);
            SessionExosuitSlotIDs = (string[])Array.CreateInstance(typeof(string), SEConfig.MAXSLOTS + 2);

            for (int i = 0; i < SEConfig.MAXSLOTS + 2; i++)
            {
                if (i == SEConfig.MAXSLOTS)
                {
                    SessionSeamothSlotIDs[i] = ExpandedSeamothSlotIDs[12];
                }
                else if (i == SEConfig.MAXSLOTS + 1)
                {
                    SessionSeamothSlotIDs[i] = ExpandedSeamothSlotIDs[13];
                }
                else
                {
                    SessionSeamothSlotIDs[i] = ExpandedSeamothSlotIDs[i];
                }
            }

            for (int i = 0; i < SEConfig.MAXSLOTS + 2; i++)
            {                
                SessionExosuitSlotIDs[i] = ExpandedExosuitSlotIDs[i];
            }
        }

        internal static void ExpandSlotMapping()
        {
            foreach (SlotData slotData in NewSeamothSlotsData)
            {
                Equipment.slotMapping.Add(slotData.SlotID, EquipmentType.SeamothModule);
            }

            foreach (SlotData slotData in NewSeamothArmSlotsData)
            {
                Equipment.slotMapping.Add(slotData.SlotID, (EquipmentType)NewEquipmentType.SeamothArm);
            }

            foreach (SlotData slotData in NewExosuitSlotsData)
            {
                Equipment.slotMapping.Add(slotData.SlotID, EquipmentType.ExosuitModule);
            }

            SNLogger.Log("SlotExtender", "Equipment slot mapping Patched!");
            /*
            if (!SlotMappingExpanded)
            {
                foreach (string slotID in NewSeamothSlotIDs)
                {
                    if (slotID.Equals("SeamothArmLeft"))
                    {
                        Equipment.slotMapping.Add(slotID, (EquipmentType) NewEquipmentType.SeamothArm);
                    }
                    else if (slotID.Equals("SeamothArmRight"))
                    {
                        Equipment.slotMapping.Add(slotID, (EquipmentType) NewEquipmentType.SeamothArm);
                    }
                    else
                    {
                        Equipment.slotMapping.Add(slotID, EquipmentType.SeamothModule);
                    }
                }

                foreach (string slotID in NewExosuitSlotIDs)
                {
                    Equipment.slotMapping.Add(slotID, EquipmentType.ExosuitModule);                    
                }

                SNLogger.Log($"[{SEConfig.PROGRAM_NAME}] Equipment slot mapping Patched!");
                SlotMappingExpanded = true;
            }
            */
        }

        internal static bool IsExtendedSeamothSlot(string slotName)
        {
            foreach (string slot in NewSeamothSlotIDs)
            { 
                if (slotName.Equals(slot))
                    return true;
            }

            return false;
        }

        internal static bool IsSeamothArmSlot(string slotName)
        {
            return slotName.Equals("SeamothArmLeft") || slotName.Equals("SeamothArmRight") ? true : false;
        }
    }
}
