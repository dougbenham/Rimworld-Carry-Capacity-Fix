using System.Text;
using Harmony;
using HugsLib;
using RimWorld;
using Verse;

namespace CarryCapacityFix
{
    public class Main : ModBase
    {
        public override string ModIdentifier => "CarryCapacityFix";
    }

    [HarmonyPatch(typeof(MassUtility), "Capacity")]
    public static class MassUtility_Capacity_Patch
    {
        [HarmonyPrefix]
        public static bool Capacity(ref float __result, Pawn p, StringBuilder explanation)
        {
            if (!MassUtility.CanEverCarryAnything(p))
            {
                __result = 0f;
                return false;
            }

            __result = p.BodySize * p.GetStatValue(StatDefOf.CarryingCapacity);
            if (explanation != null)
            {
                if (explanation.Length > 0)
                {
                    explanation.AppendLine();
                }
                explanation.Append("  - " + p.LabelShortCap + ": " + __result.ToStringMassOffset());
            }
            return false;
        }
    }
}
