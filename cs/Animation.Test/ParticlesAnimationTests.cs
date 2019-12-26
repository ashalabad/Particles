using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Animation.Test
{
    public class ParticlesAnimationTests
    {
        [Fact]
        public void RightMovingParticleIncrementsPosition()
        {
            var particle = Particle.R(10).Move(5);
            Assert.NotNull(particle);
            Assert.Equal(1, particle.Vector);
            Assert.Equal(15, particle.Position);
        }
        [Fact]
        public void LeftMovingParticleDecrementsPosition()
        {
            var particle = Particle.L(3).Move(5);
            Assert.NotNull(particle);
            Assert.Equal(-1, particle.Vector);
            Assert.Equal(-2, particle.Position);
        }
        [Fact]
        public void ChamberWithNoParticlesIsEmpty()
        {
            var chamber = new Chamber(10);
            Assert.True(chamber.IsEmpty);
        }
        [Fact]
        public void MovingChamberMovesParticles()
        {
            var chamber = new Chamber(7,new[] { 
                Particle.R(2),
                Particle.L(4)
            }).Move(3);
            Assert.NotNull(chamber);
            Assert.False(chamber.IsEmpty);
            Assert.Equal(2, chamber.Particles.Length);
            var particles = chamber.Particles.OrderBy(p => p.Position).ToArray();
            Assert.Equal(-1, particles[0].Vector);
            Assert.Equal(1, particles[0].Position);
            Assert.Equal(1, particles[1].Vector);
            Assert.Equal(5, particles[1].Position);
        }
        [Fact]
        public void RightMovingParticleLeavesChamber()
        {
            var chamber = new Chamber(7, new[] {
                Particle.R(2)
            }).Move(3).Move(4);
            Assert.NotNull(chamber);
            Assert.True(chamber.IsEmpty);
        }
        [Fact]
        public void LeftMovingParticleLeavesChamber()
        {
            var chamber = new Chamber(7, new[] {
                Particle.L(4)
            }).Move(3).Move(2);
            Assert.NotNull(chamber);
            Assert.True(chamber.IsEmpty);
        }
        [Fact]
        public void EmptyChamberRendersCorrectString()
        {
            Assert.Equal(".......", Animator.Render(new Chamber(7)));
        }
        [Fact]
        public void ChamberWithParticlesRendersCorrectString()
        {
            Assert.Equal("..x...x", Animator.Render(new Chamber(7,new[] { 
                Particle.L(2),
                Particle.R(6)
            })));
        }
        [Fact]
        public void CorrectStringOneParticleCreatesChamber()
        {
            var chamber = Animator.Parse("..R....");
            Assert.NotNull(chamber);
            Assert.Equal(7u, chamber.Width);
            Assert.Equal(1, chamber.Particles?.Length);
            Assert.Equal(1, chamber.Particles[0].Vector);
            Assert.Equal(2, chamber.Particles[0].Position);
        }

        [Fact]
        public void NegativeSpeedProducesArgumentException()
        {
            Animator a = new Animator();
            Assert.Throws<ArgumentException>(() => a.Animate(-1, "..."));
        }
        [Fact]
        public void ZeroSpeedProducesArgumentException()
        {
            Animator a = new Animator();
            Assert.Throws<ArgumentException>(() => a.Animate(0, "..."));
        }
        [Fact]
        public void OverMaxSpeedProducesArgumentException()
        {
            Animator a = new Animator();
            Assert.Throws<ArgumentException>(() => a.Animate(11, "..."));
        }
        [Fact]
        public void InvalidCharactersProduceArgumentException()
        {
            Animator a = new Animator();
            Assert.Throws<ArgumentException>(() => a.Animate(5, "..r.t.l.."));
        }
        [Fact]
        public void StringShouldNotBeNullOrEmpty()
        {
            Animator a = new Animator();
            Assert.Throws<ArgumentException>(() => a.Animate(5, string.Empty));
            Assert.Throws<ArgumentException>(() => a.Animate(5, null));
        }
        [Fact]
        public void ParticlesStringCannotBeMoreThan50Characters()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 55; i++)
                sb.Append('.');
            Animator a = new Animator();
            Assert.Throws<ArgumentException>(() => a.Animate(5, sb.ToString()));
        }

        [Fact]
        public void SimpleRParticleAnimation()
        {
            string[] expectedPatterns = { 
                "..x....",
                "....x..",
                "......x",
                "......."
            };
            Animator a = new Animator();
            var animation = a.Animate(2, "..R....");
            Assert.Equal(expectedPatterns, animation);
        }
        [Fact]
        public void AnimateMultipleElementsS1()
        {
            string[] expectedPatterns = {
                "xx..xxx",
                ".x.xx..",
                "x.....x",
                "......."
            };
            Animator a = new Animator();
            var animation = a.Animate(3, "RR..LRL");
            Assert.Equal(expectedPatterns, animation);
        }
        [Fact]
        public void AnimateMultipleElementsS2()
        {
            string[] expectedPatterns = {
                "xxxx.xxxx",
                "x..x.x..x",
                ".x.x.x.x.",
                ".x.....x.",
                ".........",
            };
            Animator a = new Animator();
            var animation = a.Animate(2, "LRLR.LRLR");
            Assert.Equal(expectedPatterns, animation);
        }

        [Fact]
        public void AnimateMultipleElementsS3()
        {
            string[] expectedPatterns = {
                "xxxxxxxxxx",
                ".........."
            };
            Animator a = new Animator();
            var animation = a.Animate(10, "RLRLRLRLRL");
            Assert.Equal(expectedPatterns, animation);
        }
        [Fact]
        public void AnimateMultipleElementsS4()
        {
            string[] expectedPatterns = {
                "..."
            };
            Animator a = new Animator();
            var animation = a.Animate(1, "...");
            Assert.Equal(expectedPatterns, animation);
        }

        [Fact]
        public void AnimateMultipleElementsS5()
        {
            string[] expectedPatterns = {
                "xxxx.xx.xxx.x.xxxx.", 
                "..xxx..x..xx.x..xx.", 
                ".x.xx.x.x..xx.xx.xx", 
                "x.x.xx...x.xxxxx..x", 
                ".x..xxx...x..xx.x..", 
                "x..x..xx.x.xx.xx.x.", 
                "..x....xx..xx..xx.x",
                ".x.....xxxx..x..xx.",
                "x.....x..xx...x..xx",
                ".....x..x.xx...x..x",
                "....x..x...xx...x..",
                "...x..x.....xx...x.",
                "..x..x.......xx...x",
                ".x..x.........xx...",
                "x..x...........xx..",
                "..x.............xx.",
                ".x...............xx",
                "x.................x",
                "...................",
            };
            Animator a = new Animator();
            var animation = a.Animate(1, "LRRL.LR.LRR.R.LRRL.");

            Assert.Equal(expectedPatterns, animation);
        }

    }
}
