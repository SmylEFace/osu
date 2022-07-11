// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Input.Bindings;
using osu.Game.Overlays;

namespace osu.Game.Screens.Menu
{
    public class ExitConfirmOverlay : HoldToConfirmOverlay, IKeyBindingHandler<GlobalAction>
    {
        protected override bool AllowMultipleFires => true;

        public void Abort() => AbortConfirm();

        public ExitConfirmOverlay()
            : base(0.7f)
        {
        }

        public bool OnPressed(KeyBindingPressEvent<GlobalAction> e)
        {
            if (e.Repeat)
                return false;

            if (e.Action == GlobalAction.Back)
            {
                BeginConfirm();
                return true;
            }

            return false;
        }

        public void OnReleased(KeyBindingReleaseEvent<GlobalAction> e)
        {
            if (e.Action == GlobalAction.Back)
            {
                if (!Fired)
                    AbortConfirm();
            }
        }
    }
}
