// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Graphics;
using osuTK.Graphics;
using osu.Framework.Allocation;
using System;

namespace osu.Game.Screens.Play
{
    public class FailOverlay : GameplayMenuOverlay
    {
        public override string Header => "failed";
        private string[] fq =
        {
            "you're dead, try again?",
            "that was one of my favorite songs!",
            "so close!",
            "you'll get it next time!",
            "you were doing so good >_<",
            "keep up the good work!",
            "oh no! ;-;",
            "",

        };
        public override string Description => fq[new Random().Next(fq.Length)];

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AddButton("Retry", colours.YellowDark, () => OnRetry?.Invoke());
            AddButton("Quit", new Color4(170, 27, 39, 255), () => OnQuit?.Invoke());
        }
    }
}
