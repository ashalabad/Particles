/**
  * Chamber State
  * @param width
  * @param particles
  */
class Chamber(val width:Int, val particles:List[Particle]) {
  def this(width:Int){
    this(width,List[Particle]())
  }
  /**
    * Move all particles in this chamber with
    * @param speed
    * @return new state with particles moved. some particles leave the chamber
    */
  def move(speed:Int):Chamber=
    new Chamber(width, particles.map(p=>p.move(speed)).filter(x=>x.position>=0 && x.position<width))
  def isEmpty:Boolean=particles.isEmpty
}
