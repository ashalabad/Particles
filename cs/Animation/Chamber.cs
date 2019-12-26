using System.Linq;

namespace Animation
{
    /// <summary>
    /// Immutable Chamber state
    /// </summary>
    public class Chamber
    {
        public bool IsEmpty => Particles.Length == 0;
        public uint Width { get; }
        public Particle[] Particles { get; }
        public Chamber(uint width, Particle[] particles)
        {
            Width = width;
            Particles = particles;
        }

        public Chamber(uint width):this(width, new Particle[] { })
        {
        }
        /// <summary>
        /// Moves particles in the chamber
        /// Particles are leaving the chamber if their position is less than 0 or greater than the chamber width
        /// </summary>
        /// <param name="speed"></param>
        /// <returns>New Chamber state</returns>
        public Chamber Move(int speed)
        {
            return new Chamber(Width, Particles.Select(p => p.Move(speed))
                .Where(p => p.Position >= 0 && p.Position < Width).ToArray());
        }
        
    }
}
