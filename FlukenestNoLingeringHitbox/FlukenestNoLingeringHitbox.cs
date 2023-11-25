using HKMirror.Hooks.ILHooks;
using Modding;
using MonoMod.Cil;
using System;
using UnityEngine;

namespace FlukenestNoLingeringHitbox
{
    public class FlukenestNoLingeringHitboxMod : Mod
    {
        private static FlukenestNoLingeringHitboxMod? _instance;

        internal static FlukenestNoLingeringHitboxMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(FlukenestNoLingeringHitboxMod)} was never constructed");
                }
                return _instance;
            }
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public FlukenestNoLingeringHitboxMod() : base("FlukenestNoLingeringHitbox")
        {
            _instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");

            ILSpellFluke.BurstSequence += (il) =>
            {
                ILCursor cursor = new ILCursor(il).Goto(0);

                cursor.TryGotoNext(
                    i => i.MatchLdcR4(1),
                    i => i.MatchNewobj<WaitForSeconds>());
                cursor.GotoNext();
                cursor.EmitDelegate<Func<float, float>>(seconds => 0f);
            };

            Log("Initialized");
        }
    }
}
