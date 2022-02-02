// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Audio;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Skinning;
using osuTK.Graphics;

namespace osu.Game.Screens.Play
{
    public class PauseOverlay : GameplayMenuOverlay
    {
        public Action OnResume;
        private string desc;

        public PauseOverlay(string desc)
        {
            this.desc = desc;
        }

        public override bool IsPresent => base.IsPresent || pauseLoop.IsPlaying;

        public override string Header => "paused";
        public override string Description => desc;

        public void SetDescription(string description)
        {
            desc = description;
            var index0 = ((FillFlowContainer)Children[1]);
            var index1 = ((FillFlowContainer)index0.Children[0]);
            var setText = ((OsuSpriteText)index1.Children[1]);
            setText.Text = Description;
        }

        private SkinnableSound pauseLoop;

        protected override Action BackAction => () => InternalButtons.Children.First().TriggerClick();

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AddButton("Continue", colours.Green, () => OnResume?.Invoke());
            AddButton("Retry", colours.YellowDark, () => OnRetry?.Invoke());
            AddButton("Quit", new Color4(170, 27, 39, 255), () => OnQuit?.Invoke());

            AddInternal(pauseLoop = new SkinnableSound(new SampleInfo("Gameplay/pause-loop"))
            {
                Looping = true,
                Volume = { Value = 0 }
            });
        }

        protected override void PopIn()
        {
            base.PopIn();

            pauseLoop.VolumeTo(1.0f, TRANSITION_DURATION, Easing.InQuint);
            pauseLoop.Play();
        }

        protected override void PopOut()
        {
            base.PopOut();

            pauseLoop.VolumeTo(0, TRANSITION_DURATION, Easing.OutQuad).Finally(_ => pauseLoop.Stop());
        }
    }
}
