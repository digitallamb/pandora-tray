using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PandoraTray;

namespace PandoraOneMediaKeys
{
    public class PandoraController
    {
        /// <summary>
        /// Pokes the pandora.
        /// </summary>
        /// <param name="action">The action.</param>
        private static void PokePandora(PandoraActions action)
        {
            if (PandoraProcess.IsRunning())
            {
                switch (action)
                {
                    case PandoraActions.PlayPause:
                        PandoraProcess.SendKey(Keys.Space);
                        break;
                    case PandoraActions.Skip:
                        PandoraProcess.SendKey(Keys.Right);
                        break;
                    case PandoraActions.Like:
                        PandoraProcess.SendKey(Keys.Add);
                        break;
                    case PandoraActions.Dislike:
                        PandoraProcess.SendKey(Keys.Subtract);
                        break;
                    case PandoraActions.RaiseVolume:
                        PandoraProcess.SendKey(Keys.Up);
                        break;
                    case PandoraActions.LowerVolume:
                        PandoraProcess.SendKey(Keys.Down);
                        break;
                    case PandoraActions.FullVolume:
                        PandoraProcess.SendKey(Keys.Up, true);
                        break;
                    case PandoraActions.Mute:
                        PandoraProcess.SendKey(Keys.Down, true);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Plays or Pauses
        /// </summary>
        public static void PlayPause()
        {
            PokePandora(PandoraActions.PlayPause);
        }

        /// <summary>
        /// Skips to the next song.
        /// </summary>
        public static void Next()
        {
            PokePandora(PandoraActions.Skip);
        }

        /// <summary>
        /// Likes the current song.
        /// </summary>
        public static void Like()
        {
            PokePandora(PandoraActions.Like);
        }

        /// <summary>
        /// Dislikes the current song.
        /// </summary>
        public static void Dislike()
        {
            PokePandora(PandoraActions.Dislike);
        }

        /// <summary>
        /// Raises the volume.
        /// </summary>
        public static void RaiseVolume()
        {
            PokePandora(PandoraActions.RaiseVolume);
        }

        /// <summary>
        /// Lowers the volume.
        /// </summary>
        public static void LowerVolume()
        {
            PokePandora(PandoraActions.LowerVolume);
        }

        /// <summary>
        /// Full volume.
        /// </summary>
        public static void FullVolume()
        {
            PokePandora(PandoraActions.FullVolume);
        }

        /// <summary>
        /// Mutes the volume.
        /// </summary>
        public static void MuteVolume()
        {
            PokePandora(PandoraActions.Mute);
        }
    }
}
