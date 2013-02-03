using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandoraTray
{
    public enum PandoraActions
    {
        /// <summary>
        /// Toggle Play / Pause
        /// </summary>
        PlayPause,
        /// <summary>
        /// Skip to the next song
        /// </summary>
        Skip,
        /// <summary>
        /// Like the current song
        /// </summary>
        Like,
        /// <summary>
        /// Disklike the current song and skip to next
        /// </summary>
        Dislike,
        /// <summary>
        /// Raise the volume
        /// </summary>
        RaiseVolume,
        /// <summary>
        /// Lower the volume
        /// </summary>
        LowerVolume,
        /// <summary>
        /// Raise the volume to full
        /// </summary>
        FullVolume,
        /// <summary>
        /// Mute the volume
        /// </summary>
        Mute
    }
}
