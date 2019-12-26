
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animation
{
    /// <summary>
    /// Animator class implements the assignment requirements
    /// </summary>
    public class Animator
    {
        /// <summary>
        /// Parse and animate particles in a chamber
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="particles"></param>
        /// <returns></returns>
        public string[] Animate(int speed, string particles)
        {
            if (speed <= 0 || speed > 10)
                throw new ArgumentException($"Invalid speed value:{speed}. It must be within 1 and 10. ");
            if (string.IsNullOrEmpty(particles))
                throw new ArgumentException("The argument 'particles' is null or empty.");
            if(particles.Length>50)
                throw new ArgumentException("Maximum argument 'particles' length is 50 characters");
            Chamber c = Parse(particles);
            List<string> s = new List<string> { Render(c) };
            if(!c.IsEmpty)
                do
                {
                    c = c.Move(speed);
                    s.Add(Render(c));
                } while (!c.IsEmpty);
            return s.ToArray();
        }
        /// <summary>
        /// Parse "Chamber/Particles" definition defined by Animate method requirements
        /// </summary>
        /// <param name="template"></param>
        /// <returns><see cref="Chamber"/></returns>
        public static Chamber Parse(string template)
        {
            uint width = (uint)template.Length;
            return template.Aggregate(new Tuple<int,Chamber>(0,new Chamber(width)),(a, c) => {
                switch (c)
                {
                    case '.': 
                        return new Tuple<int, Chamber>(a.Item1 + 1, a.Item2);
                    case 'R': 
                        return new Tuple<int, Chamber>(a.Item1 + 1, 
                            new Chamber(a.Item2.Width,a.Item2.Particles.Append(Particle.R(a.Item1)).ToArray()));
                    case 'L': 
                        return new Tuple<int, Chamber>(a.Item1 + 1, 
                            new Chamber(a.Item2.Width, a.Item2.Particles.Append(Particle.L(a.Item1)).ToArray()));
                    default:
                        throw new ArgumentException($"{template} contains invalid values. Only .,R and L are allowed,");
                }
            }).Item2;
        }
        /// <summary>
        /// Renders Camber according to Animate method requirements
        /// </summary>
        /// <param name="chamber"></param>
        /// <returns>String representation of <see cref="Chamber"/></returns>
        public static string Render(Chamber chamber)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<chamber.Width; i++)
                sb.Append(chamber.Particles.Any(p => p.Position == i) ? 'x' : '.');
            return sb.ToString();
        }
    }
}
