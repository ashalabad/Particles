class Animation {

  private def onAnimate(speed:Int,c:Chamber,acc:List[String]):List[String]={
    val list=acc:+Animation.render(c)
    if(c.isEmpty)
      return list
    else
      return onAnimate(speed,c.move(speed),list)
  }

  /**
    * Animate the particles
    * @param speed speed of movement, >0
    * @param particles string representation of particles
    * @return
    */
  def animate(speed:Int, particles:String):List[String]= {
    require(speed>0 && speed<=10,"speed argument should be beween 1 and 10")
    require(particles.size>0 && particles.size<=50,"particles string should not be empty and should be less or equals 50 characters")
    onAnimate(speed, Animation.parse(particles), List[String]())
  }
}

object Animation {

  /**
    * Parse string representation of particles
    * @param particles
    * @return an instance of Chamber
    */
  def parse(particles:String):Chamber=
    particles.foldLeft[Chamber](new Chamber(0)) { (chamber, chr)=>
      chr match {
        case '.'=>new Chamber(chamber.width+1, chamber.particles)
        case 'R'=>new Chamber(chamber.width+1, chamber.particles:+Particle.R(chamber.width))
        case 'L'=>new Chamber(chamber.width+1, chamber.particles:+Particle.L(chamber.width))
        case _ =>throw new IllegalArgumentException(s"Invalid character $chr")
      }
    }

  /**
    * Render a Chamber
    * @param c
    * @return
    */
  def render(c:Chamber):String={
    val sb=new StringBuilder()
    for(i<-0 to c.width-1) {
      if(c.particles.exists(p=>p.position==i))
        sb.addOne('x')
      else
        sb.addOne('.')
    }
    sb.result()
  }
}
