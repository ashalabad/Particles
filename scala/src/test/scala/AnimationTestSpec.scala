import org.scalatest.flatspec.AnyFlatSpec

class AnimationTestSpec extends AnyFlatSpec {
  "Right Moving particle" should "increment its position" in {
      val p=Particle.R(10).move(5)
      assert(p.vector==1)
      assert(p.position==15)
  }

  "Left Moving particle" should "decrement its position" in {
    val p=Particle.L(10).move(5)
    assert(-1==p.vector)
    assert(5==p.position)
  }

  "Chamber" should "contain no particles when empty" in {
    val c=new Chamber(10)
    assert(c.isEmpty)
  }

  "Chamber" should "move particles when moved" in {
    val chamber=new Chamber(7,List[Particle]():+Particle.R(2):+Particle.L(4)).move(3)
    assert(!chamber.isEmpty)
    assert(chamber.particles.size==2)
    assert(chamber.particles(0).position==5)
    assert(chamber.particles(0).vector==1)
    assert(chamber.particles(1).position==1)
    assert(chamber.particles(1).vector==(-1))
  }

  "Right Moving particle" should "leave the chamber" in {
    val chamber=new Chamber(7,List[Particle]():+Particle.R(2)).move(3).move(4)
    assert(chamber.isEmpty)
  }

  "Left Moving particle" should "leave the chamber" in {
    val chamber=new Chamber(7,List[Particle]():+Particle.L(4)).move(3).move(2)
    assert(chamber.isEmpty)
  }

  "Render Empty chamber" should "render dot-filled string" in {
    val s=Animation.render(new Chamber(7))
    assert("......."==s)
  }

  "Render chamber with Particles" should "render correct string" in {
    val s=Animation.render(new Chamber(7,List[Particle]():+Particle.R(2):+Particle.L(6)))
    assert("..x...x"==s)
  }

  "Parse particles string" should "instantiate a chamber with particles" in {
    val chamber=Animation.parse("..R....")
    assert(7==chamber.width)
    assert(1==chamber.particles.size)
    assert(1==chamber.particles(0).vector)
    assert(2==chamber.particles(0).position)
  }

  "Animation speed" should "not allow negative values" in {
     assertThrows[IllegalArgumentException](new Animation().animate(-2,"..R...."))
  }
  "Animation speed" should "not allow zero" in {
    assertThrows[IllegalArgumentException](new Animation().animate(0,"..R...."))
  }
  "Animation speed" should "not allow values greater than 10" in {
    assertThrows[IllegalArgumentException](new Animation().animate(11,"..R...."))
  }
  "Particles string" should "not be empty" in {
    assertThrows[IllegalArgumentException](new Animation().animate(5,""))
  }
  "Particles string length" should "be less or equal 50 characters" in {
    val sb=new StringBuilder();
    for(i<-0 to 55) sb.addOne('.')
    assertThrows[IllegalArgumentException](new Animation().animate(5,sb.result()))
  }

  "One Particle Animation" should "move the particle through the chamber" in {
    val expected=List[String](
      "..x....",
      "....x..",
      "......x",
      ".......")
    val res=new Animation().animate(2,"..R....")
    assert(4==res.size)
    assert(expected==res)
  }

  "Multi Particles animation" should "work for Scenario 1" in {
    val expected=List[String](
      "xx..xxx",
      ".x.xx..",
      "x.....x",
      ".......")
    val res=new Animation().animate(3, "RR..LRL")
    assert(expected==res)
  }

  "Multi Particles animation" should "work for Scenario 2" in {
    val expected=List[String](
      "xxxx.xxxx",
    "x..x.x..x",
    ".x.x.x.x.",
    ".x.....x.",
    ".........")
    val res=new Animation().animate(2, "LRLR.LRLR")
    assert(expected==res)
  }

  "Multi Particles animation" should "work for Scenario 3" in {
    val expected=List[String](
      "xxxxxxxxxx",
      "..........")
    val res=new Animation().animate(10, "RLRLRLRLRL")
    assert(expected==res)
  }

  "Empty chamber animation" should "return single line" in {
    val expected=List[String](
      "...")
    val res=new Animation().animate(1, "...")
    assert(expected==res)
  }

  "Multi Particles animation" should "work for Scenario 5" in {
    val expected=List[String](
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
    "...................")
    val res=new Animation().animate(1, "LRRL.LR.LRR.R.LRRL.")
    assert(expected==res)
  }

}
