defmodule Particle do
  @moduledoc """
  Particle 
  """
  @doc """
  Particle structure
  """
  defstruct vector: 1 , position: 0 
  @doc """
  Move particle
  """
  def move(particle, speed) when is_map(particle) do
    %Particle{vector: particle.vector, position: particle.position+particle.vector*speed}
  end
  @doc """
  Produce left-moving particle
  """
  def left(position) when is_integer(position) do
    %Particle{vector: -1, position: position}
  end
  @doc """
  Produce right-moving particle
  """
  def right(position) when is_integer(position) do
    %Particle{vector: 1, position: position}
  end
  
end

defmodule Chamber do
  @moduledoc """
  Chamber 
  """
  defstruct width: 0, particles: []
  @doc """
  Move all particles in the chamber, retruns new chamber state
  """
  def move(chamber,speed) when is_map(chamber) and is_integer(speed) do 
     %Chamber{width: chamber.width, particles: chamber.particles |> Enum.map(fn p->Particle.move(p,speed) end) |> Enum.filter(fn x->x.position>=0 && x.position<chamber.width end)}
  end

  def is_empty(chamber) when is_map(chamber) do
    Enum.count(chamber.particles)==0
  end
end

defmodule Animator do 
  @moduledoc """
  Animator
  """
  def parse(input)  do 
   input |> String.codepoints() 
   |> List.foldl(%Chamber{width: 0, particles: []}, 
    fn x,acc->
        case x do
          "."-> %Chamber{width: acc.width+1, particles: acc.particles} 
          "R"-> %Chamber{width: acc.width+1, particles: acc.particles++[Particle.right(acc.width)]} 
          "L"-> %Chamber{width: acc.width+1, particles: acc.particles++[Particle.left(acc.width)]} 
          _-> throw("Bad character")
        end        
    end)
  end

  defp render_recusive(chamber,acc, position) when is_map(chamber) and is_integer(position) do 
    if position == chamber.width do acc
      else
        if Enum.any?(chamber.particles, fn x->x.position==position end) do
            render_recusive(chamber,acc<>"x", position+1)
          else  
            render_recusive(chamber,acc<>".", position+1)
          end
      end
  end

  @doc """
  Render a chamber as a string
  """
  def render(chamber) when is_map(chamber) do
    render_recusive(chamber,"",0)
  end

  defp animate_recursive(chamber, acc, speed) do
    if Enum.count(chamber.particles) == 0 do
      acc
    else
      ch=Chamber.move(chamber,speed)      
      animate_recursive(ch,acc++[Animator.render(ch)],speed)
    end  
  end
  @doc """
  Parse input string and animate particles in the chamber
  """
  def animate(speed,input) do
    chamber=parse(input)
    acc=[render(chamber)]
    animate_recursive(chamber,acc,speed)
  end
end
