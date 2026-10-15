using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SinsEntityConverter
{
    public class SquadDefinition : Def
    {
        public string? Header = "TXT2";
        public EntityInt? SinsArchiveVersion;
        public string? entityType;
        public string? defaultAutoAttackRange;
        public EntityBool? defaultAutoAttackOn;
        public EntityBool? prefersToFocusFire;
        public EntityBool? usesFighterAttack;
        public string? hudIcon;
        public string? smallHudIcon;
        public string? infoCardIcon;
        public string? fighterEntityDef;
        public string? fighterIllusionEntityDef;
        public EntityFloat? scuttleTime;
        public EntityFloat? fighterConstructionTime;
        public EntityFloat? decayWithoutOwnerRate;
        public EntityInt? baseMaxNumFighters;
        public string? role;
        public string? statCountType;
        public string? mainViewIcon;
        public string? picture;
        public string? launchIcon;
        public string? dockIcon;
        public string? nameStringID;
        public string? descriptionStringID;
        public Prerequisites? researchPrerequisites;
        public List<NumSoundsFor>? NumSounds;
        public EntityFloat? minShadow;
        public EntityFloat? maxShadow;
        public List<string>? abilities;



        public override bool Apply(string key, string value)
        {
            switch (key)
            {
                case "SinsArchiveVersion":
                    SinsArchiveVersion = new EntityInt(value);
                    return true;

                case "entityType":
                    entityType = value;
                    return true;

                case "defaultAutoAttackRange":
                    defaultAutoAttackRange = value;
                    return true;

                case "defaultAutoAttackOn":
                    defaultAutoAttackOn = new EntityBool(value);
                    return true;

                case "prefersToFocusFire":
                    prefersToFocusFire = new EntityBool(value);
                    return true;

                case "usesFighterAttack":
                    usesFighterAttack = new EntityBool(value);
                    return true;

                case "hudIcon":
                    hudIcon = value;
                    return true;

                case "smallHudIcon":
                    smallHudIcon = value;
                    return true;

                case "infoCardIcon":
                    infoCardIcon = value;
                    return true;

                case "fighterEntityDef":
                    fighterEntityDef = value;
                    return true;

                case "fighterIllusionEntityDef":
                    fighterIllusionEntityDef = value;
                    return true;

                case "scuttleTime":
                    scuttleTime = new EntityFloat(value);
                    return true;

                case "fighterConstructionTime":
                    fighterConstructionTime = new EntityFloat(value);
                    return true;

                case "decayWithoutOwnerRate":
                    decayWithoutOwnerRate = new EntityFloat(value);
                    return true;

                case "baseMaxNumFighters":
                    baseMaxNumFighters = new EntityInt(value);
                    return true;

                case "role":
                    role = value;
                    return true;

                case "statCountType":
                    statCountType = value;
                    return true;

                case "mainViewIcon":
                    mainViewIcon = value;
                    return true;

                case "picture":
                    picture = value;
                    return true;

                case "launchIcon":
                    launchIcon = value;
                    return true;

                case "dockIcon":
                    dockIcon = value;
                    return true;

                case "nameStringID":
                    nameStringID = value;
                    return true;

                case "descriptionStringID":
                    descriptionStringID = value;
                    return true;

                case "researchPrerequisites":
                    researchPrerequisites = new();
                    return true;

                case "NumResearchPrerequisites":
                    researchPrerequisites!.NumResearchPrerequisites = new EntityInt(value);
                    return true;

                case "ResearchPrerequisite":
                    researchPrerequisites!.ResearchPrerequisite = new();
                    researchPrerequisites!.ResearchPrerequisite.Add(new Research());
                    return true;

                case "Subject":
                    researchPrerequisites!.ResearchPrerequisite![^1].Subject = value;
                    return true;

                case "Level":
                    researchPrerequisites!.ResearchPrerequisite![^1].Level = new EntityInt(value);
                    return true;

                case "RequiredFactionNameID":
                    researchPrerequisites!.RequiredFactionNameID = value;
                    return true;

                case "RequiredCompletedResearchSubjects":
                    researchPrerequisites!.RequiredCompletedResearchSubjects = new EntityInt(value);
                    return true;

                case string k when k.StartsWith("NumSoundsFor:"):
                    NumSounds ??= new();
                    NumSounds.Add(new NumSoundsFor
                    {
                        For = new() { { k["NumSoundsFor:".Length..], new EntityInt(value) } },
                        SoundID = new()
                    });
                    return true;

                case "SoundID":
                    NumSounds![^1].SoundID!.Add(value);
                    return true;

                case "minShadow":
                    minShadow = new EntityFloat(value);
                    return true;

                case "maxShadow":
                    maxShadow = new EntityFloat(value);
                    return true;

                case "ability:0":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "ability:1":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "ability:2":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "ability:3":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "ability:4":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                default:
                    return false;
            }
        }
    }
}
