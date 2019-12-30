defmodule ParticlesTest do
  use ExUnit.Case
  doctest Particle

  test "produce left-moving particle" do
    assert Particle.left(10) == %Particle{vector: -1, position: 10}
  end

  test "produce right-moving particle" do
    assert Particle.right(10) == %Particle{vector: 1, position: 10}
  end

  test "move left-moving particle decreases position" do
    particle=Particle.left(2)  
    assert particle== %Particle{vector: -1, position: 2}
    assert Particle.move(particle,4) == %Particle{vector: -1, position: -2}
  end

  test "chamber can be created with particles" do
    chamber=%Chamber{width: 10, particles: [
      Particle.left(1),
      Particle.right(3),
      Particle.left(9)
    ]} 
    assert chamber.width == 10
    assert Enum.count(chamber.particles) == 3
    assert Enum.at(chamber.particles,0) == %Particle{vector: -1, position: 1}
    assert Enum.at(chamber.particles,1) == %Particle{vector: 1, position: 3}
    assert Enum.at(chamber.particles,2) == %Particle{vector: -1, position: 9}
  end

  test "particles in chamber can be moved" do
    chamber=%Chamber{width: 10, particles: [
      Particle.left(1),
      Particle.right(3),
      Particle.left(9)
    ]} |> Chamber.move(4)
    assert chamber.width == 10
    assert Enum.count(chamber.particles) == 2
    assert Enum.at(chamber.particles,0) == %Particle{vector: 1, position: 7}
    assert Enum.at(chamber.particles,1) == %Particle{vector: -1, position: 5}
  end

  test "chamber can be rendered" do
    rendered=%Chamber{width: 10, particles: [
        Particle.left(1),
        Particle.right(3),
        Particle.left(9)
      ]} |> Animator.render
    assert rendered == ".x.x.....x"  
  end

  test "input string can be parsed to chamber" do
    chamber=Animator.parse(".L.R.....L")
    assert chamber.width == 10
    assert Enum.count(chamber.particles) == 3
    assert Enum.at(chamber.particles,0) == %Particle{vector: -1, position: 1}
    assert Enum.at(chamber.particles,1) == %Particle{vector: 1, position: 3}
    assert Enum.at(chamber.particles,2) == %Particle{vector: -1, position: 9}
  end

  test "animation for ..R.... with speed of 2" do
    expected=[
      "..x....",
      "....x..",
      "......x",
      "......."
    ]
    assert expected==Animator.animate(2,"..R....")
  end

  test "animate RR..LRL with speed of 3" do
    expected=[
      "xx..xxx",
      ".x.xx..",
      "x.....x",
      "......."
    ]
    assert expected==Animator.animate(3,"RR..LRL")
  end

  test "animate LRLR.LRLR with speed of 2" do
    expected=[
      "xxxx.xxxx",
      "x..x.x..x",
      ".x.x.x.x.",
      ".x.....x.",
      "........."
    ]
    assert expected==Animator.animate(2,"LRLR.LRLR")
  end

  test "animate RLRLRLRLRL with speed of 10" do
    expected=[
      "xxxxxxxxxx",
      ".........."
    ]
    assert expected==Animator.animate(10,"RLRLRLRLRL")
  end

  test "animate ... with speed of 1" do
    expected=[
      "..."
    ]
    assert expected==Animator.animate(1,"...")
  end

  test "animate LRRL.LR.LRR.R.LRRL. with speed of 1" do
    expected=[
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
    ]
    assert expected==Animator.animate(1,"LRRL.LR.LRR.R.LRRL.")
  end
end
