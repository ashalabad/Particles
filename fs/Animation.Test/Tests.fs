module Tests

open System
open Xunit
open System.Text

type Direction =
    | Left 
    | Right

type Particle(vector:Direction, position:int32)=
    member this.Vector=vector
    member this.Position=position
    member this.move(distance:int32):Particle=match this.Vector with 
                                                | Left->Particle(this.Vector,this.Position-distance) 
                                                | Right->Particle(this.Vector,this.Position+distance)

type Chamber(width:int32, particles:Particle seq)=
    member this.Width=width
    member this.Particles=particles
    member this.moveParticles(distance:int32):Particle seq=particles 
                                                                |> Seq.map (fun x->x.move(distance)) 
                                                                |> Seq.filter (fun x->x.Position >=0 && x.Position<this.Width)
    member this.move(distance:int32):Chamber=Chamber(this.Width,this.moveParticles(distance))
    member this.empty=this.Particles |> Seq.isEmpty


let aggregate(sb:StringBuilder, i:int32, particles:Particle seq):unit=
      let particle=match particles |> Seq.tryFind(fun x->x.Position.Equals(i)) with
                        | Some p ->'*'
                        | None ->'.'
      sb.Append(particle) |> ignore

let render(c:Chamber):string=
    let sb=StringBuilder()
    let v=[0..c.Width-1] 
        |> List.iter(fun x->aggregate(sb,x,c.Particles))
    sb.ToString()

let rec animation(speed:int32,c:Chamber, acc:string list):string list=
    let l=acc @ [render(c)]
    if c.empty then
        l
    else animation(speed,c.move(speed),l)    

let parseChar(a:Chamber):char->Chamber=
    let m(c:char):Chamber=match c with
                            |'R'->Chamber(a.Width+1,a.Particles |>Seq.append [Particle(Right,a.Width)])
                            |'L'->Chamber(a.Width+1,a.Particles |>Seq.append [Particle(Left,a.Width)])
                            |'.'->Chamber(a.Width+1,a.Particles)
                            |_->a
    m

let parse(s:string):Chamber=
    s.ToCharArray() 
            |> Seq.fold(fun a c->parseChar a c) (Chamber(0, seq []))

let animate(patterns:string):int32->string list=
    let chamber=parse patterns
    let mv(speed:Int32):string list=
        animation(speed,chamber,[])
    mv

[<Fact>]
let ``Should animate particls in chamber`` () =
    let result=animate "...RL..." 1
    Assert.Equal(6, result |> List.length)

[<Fact>]
let ``Should move a particle left`` () =
    let p=Particle(Left,10)
    let moved=p.move 5 
    Assert.Equal(5,moved.Position)

[<Fact>]
let ``Should move a particle right`` ()=
    let p=Particle(Right,10)
    let moved=p.move 5 
    Assert.Equal(15,moved.Position)

[<Fact>]
let ``Should render a chamber with particles`` ()=
    let chamber=Chamber(10,seq [Particle(Left,2); Particle(Right,4);])
    let s=render chamber
    Assert.Equal("..*.*.....",s)


