/***
  * Partical state defines the position in chamber and moving direction
  * @param vector
  * @param position
  */
class Particle private (val vector:Int, val position:Int) {
  /**
    * Move a particle and return new particle state
    * @param speed
    * @return a Particle with new position
    */
  def move(speed:Int):Particle=new Particle(vector,position+speed*vector)
}

/**
  * Companion object for Particle
  */
object Particle {
  /**
    * Create Right-moving particle
    * @param position in Chamber
    * @return new instance of Particle
    */
  def R(position:Int):Particle=new Particle(1,position)

  /**
    * Create Left-moving particle
    * @param position in Chamber
    * @return new instance of Particle
    */
  def L(position:Int):Particle=new Particle(-1,position)
}
