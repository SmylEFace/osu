// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Edit.Compose.Components
{
    /// <summary>
    /// Represents a display composite containing and managing the visibility state of the selection box's drag handles.
    /// </summary>
    public class SelectionBoxDragHandleContainer : CompositeDrawable
    {
        private Container<SelectionBoxScaleHandle> scaleHandles;
        private Container<SelectionBoxRotationHandle> rotationHandles;

        private readonly List<SelectionBoxDragHandle> allDragHandles = new List<SelectionBoxDragHandle>();

        public new MarginPadding Padding
        {
            get => base.Padding;
            set => base.Padding = value;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                scaleHandles = new Container<SelectionBoxScaleHandle>
                {
                    RelativeSizeAxes = Axes.Both,
                },
                rotationHandles = new Container<SelectionBoxRotationHandle>
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(-12.5f),
                },
            };
        }

        public void AddScaleHandle(SelectionBoxScaleHandle handle)
        {
            bindDragHandle(handle);
            scaleHandles.Add(handle);
        }

        public void AddRotationHandle(SelectionBoxRotationHandle handle)
        {
            handle.Alpha = 0;
            handle.AlwaysPresent = true;

            bindDragHandle(handle);
            rotationHandles.Add(handle);
        }

        private void bindDragHandle(SelectionBoxDragHandle handle)
        {
            handle.HoverGained += updateRotationHandlesVisibility;
            handle.HoverLost += updateRotationHandlesVisibility;
            handle.OperationStarted += updateRotationHandlesVisibility;
            handle.OperationEnded += updateRotationHandlesVisibility;
            allDragHandles.Add(handle);
        }

        private SelectionBoxRotationHandle displayedRotationHandle;
        private SelectionBoxDragHandle activeHandle;

        private void updateRotationHandlesVisibility()
        {
            if (activeHandle?.InOperation == true || activeHandle?.IsHovered == true)
                return;

            displayedRotationHandle?.FadeOut(SelectionBoxControl.TRANSFORM_DURATION, Easing.OutQuint);
            displayedRotationHandle = null;

            activeHandle = allDragHandles.SingleOrDefault(h => h.InOperation);
            activeHandle ??= allDragHandles.SingleOrDefault(h => h.IsHovered);

            if (activeHandle != null)
            {
                displayedRotationHandle = getCorrespondingRotationHandle(activeHandle, rotationHandles);
                displayedRotationHandle?.FadeIn(SelectionBoxControl.TRANSFORM_DURATION, Easing.OutQuint);
            }
        }

        /// <summary>
        /// Gets the rotation handle corresponding to the given handle.
        /// </summary>
        [CanBeNull]
        private static SelectionBoxRotationHandle getCorrespondingRotationHandle(SelectionBoxDragHandle handle, IEnumerable<SelectionBoxRotationHandle> rotationHandles)
        {
            if (handle is SelectionBoxRotationHandle rotationHandle)
                return rotationHandle;

            return rotationHandles.SingleOrDefault(r => r.Anchor == handle.Anchor);
        }
    }
}
