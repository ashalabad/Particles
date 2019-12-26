namespace Animation
{
    /// <summary>
    /// Immutable Particle state
    /// Represents Particle position in a Chamber and its direction
    /// </summary>
    public class Particle
    {
        /// <summary>
        /// Vector defiles the moving direction o the Particle
        ///  1: Move Right
        /// -1: Move Left
        /// </summary>
        public int Vector { get; }
        /// <summary>
        /// Position in the chamber
        /// </summary>
        public int Position { get; }
        private Particle(int vector,int position)
        {
            Vector = vector;
            Position = position;
        }
        public Particle Move(int speed)
        {
            return new Particle(Vector, Position + speed * Vector);
        }
        /// <summary>
        /// Factory method that produces right-moving particle
        /// </summary>
        /// <param name="position">Position in chamber</param>
        /// <returns></returns>
        public static Particle R(int position) => new Particle(1, position);
        /// <summary>
        /// Factory method that produces left-moving particle
        /// </summary>
        /// <param name="position">Position in CHamber</param>
        /// <returns></returns>
        public static Particle L(int position) => new Particle(-1, position);
    }
}
