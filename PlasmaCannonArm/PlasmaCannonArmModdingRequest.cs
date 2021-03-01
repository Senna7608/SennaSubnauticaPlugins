﻿using UnityEngine;
using ModdedArmsHelper.API;
using ModdedArmsHelper.API.Interfaces;

namespace PlasmaCannonArm
{
    public class PlasmaCannonArmModdingRequest : IArmModdingRequest
    {
        public IExosuitArm GetExosuitArmHandler(GameObject clonedArm)
        {
            return clonedArm.AddComponent<PlasmaCannonArmHandler>();
        }        

        public ISeamothArm GetSeamothArmHandler(GameObject clonedArm)
        {
            return null;
        }

        public void SetUpArm(GameObject clonedArm, SetupHelper graphicsHelper)
        {
            GameObject elbow = ArmServices.main.objectHelper.FindDeepChild(clonedArm, "elbow");

            GameObject plasmaCannon = elbow.FindChild("torpedoLauncher");
            plasmaCannon.name = "plasmaCannon";

            Object.DestroyImmediate(plasmaCannon.FindChild("reload"));
            Object.DestroyImmediate(plasmaCannon.FindChild("torpedo_innr1"));
            Object.DestroyImmediate(plasmaCannon.FindChild("torpedo_innr2"));
            Object.DestroyImmediate(plasmaCannon.FindChild("torpedo_outr1"));
            Object.DestroyImmediate(plasmaCannon.FindChild("torpedo_outr2"));
            Object.DestroyImmediate(plasmaCannon.FindChild("TorpedoFirst"));
            Object.DestroyImmediate(plasmaCannon.FindChild("TorpedoSecond"));
            Object.DestroyImmediate(plasmaCannon.FindChild("TorpedoReload"));
            Object.DestroyImmediate(plasmaCannon.FindChild("collider"));            

            GameObject PlasmaTubeRight = plasmaCannon.FindChild("TorpedoSiloFirst");
            PlasmaTubeRight.name = "PlasmaTubeRight";
            PlasmaTubeRight.transform.localPosition = new Vector3(-1.27f, 0.02f, 0.18f);

            GameObject PlasmaTubeLeft = plasmaCannon.FindChild("TorpedoSiloSecond");
            PlasmaTubeLeft.name = "PlasmaTubeLeft";
            PlasmaTubeLeft.transform.localPosition = new Vector3(-1.27f, 0.02f, -0.18f);

            GameObject ArmRig = ArmServices.main.objectHelper.FindDeepChild(clonedArm, "ArmRig"); ;

            GameObject exosuit_arm_plasmaCannon_geo = ArmRig.FindChild("ArmModel");           

            graphicsHelper.DisableLowerArmMesh();            

            GameObject PlasmaArm = Object.Instantiate(Main.assetBundle.LoadAsset<GameObject>("PlasmaArm"), plasmaCannon.transform);
            PlasmaArm.name = "PlasmaArm";
            GameObject precursorColumnMaze = Resources.Load("WorldEntities/Environment/Precursor/Gun/Precursor_Gun_ControlRoom_CentralColumn") as GameObject;

            GameObject precursorColumn = precursorColumnMaze.transform.Find("precursor_column_maze_08_06_08_hlpr/precursor_column_maze_08_06_08_ctrl/precursor_column_maze_08_06_08/precursor_column_maze_08_06_08_glass_02_hlpr/precursor_column_maze_08_06_08_glass_02_ctrl/precursor_column_maze_08_06_08_glass_02").gameObject;

            Transform GlowTubeLeft = PlasmaArm.transform.Find("GlowTubeLeft");
            Transform GlowTubeRight = PlasmaArm.transform.Find("GlowTubeRight");

            GameObject glowLeft = Object.Instantiate(precursorColumn, GlowTubeLeft);
            glowLeft.name = "glowLeft";
            glowLeft.transform.localPosition = Vector3.zero;
            glowLeft.transform.localScale = new Vector3(1.28f, 1.43f, 7.09f);
            glowLeft.transform.localRotation = Quaternion.Euler(0, 0, 0);

            MeshRenderer glowLeftRenderer = glowLeft.GetComponent<MeshRenderer>();

            foreach (Material material in glowLeftRenderer.materials)
            {
                material.DisableKeyword("FX_ADDFOG");
            }

            GameObject glowRight = Object.Instantiate(glowLeft, GlowTubeRight);
            glowRight.name = "glowRight";
            glowRight.transform.localPosition = Vector3.zero;
            glowRight.transform.localScale = new Vector3(1.28f, 1.43f, 7.09f);
            glowRight.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Texture2D _MainTex = Main.assetBundle.LoadAsset<Texture2D>("PlasmaArm_MainTex");
            Texture2D _Illum = Main.assetBundle.LoadAsset<Texture2D>("PlasmaArm_Illum");
            Texture2D _BumpMap = Main.assetBundle.LoadAsset<Texture2D>("PlasmaArm_BumpMap");            

            MeshRenderer laserRenderer = PlasmaArm.GetComponent<MeshRenderer>();

            Shader marmoShader = Shader.Find("MarmosetUBER");

            foreach (Material material in laserRenderer.materials)
            {                
                material.shader = marmoShader;               
                material.EnableKeyword("MARMO_EMISSION");                
                material.EnableKeyword("_ZWRITE_ON");
                material.SetTexture(Shader.PropertyToID("_MainTex"), _MainTex);
                material.SetTexture(Shader.PropertyToID("_BumpMap"), _BumpMap);                
                material.SetTexture(Shader.PropertyToID("_Illum"), _Illum);
            }

            PlasmaArm.transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
            PlasmaArm.transform.localPosition = new Vector3(-0.33f, 0.20f, -0.20f);
            PlasmaArm.transform.localRotation = Quaternion.Euler(0, 270, 180);

            Object.DestroyImmediate(exosuit_arm_plasmaCannon_geo.FindChild("torpedo_innr1_geo"));
            Object.DestroyImmediate(exosuit_arm_plasmaCannon_geo.FindChild("torpedo_innr2_geo"));
            Object.DestroyImmediate(exosuit_arm_plasmaCannon_geo.FindChild("torpedo_outr1_geo"));
            Object.DestroyImmediate(exosuit_arm_plasmaCannon_geo.FindChild("torpedo_outr2_geo"));
            Object.DestroyImmediate(exosuit_arm_plasmaCannon_geo.FindChild("torpedo_reload_geo"));

            GameObject powerTransmitter = CraftData.InstantiateFromPrefab(TechType.PowerTransmitter);

            GameObject laserBeam = Object.Instantiate(powerTransmitter.GetComponent<PowerFX>().vfxPrefab, null, false);

            LineRenderer lineRenderer = laserBeam.GetComponent<LineRenderer>();

            Main.plasma_Material = Object.Instantiate(lineRenderer.material) as Material;

            Object.DestroyImmediate(powerTransmitter);

            Object.DestroyImmediate(laserBeam);            
        }
    }    
}
