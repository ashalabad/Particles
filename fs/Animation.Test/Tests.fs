module Tests


open Animation
open Xunit
open System


[<Fact>]
let ``Should animate particls in chamber`` () =
    let result=animate "...RL..." 1
    Assert.Equal(6, result |> List.length)

[<Fact>]
let ``Should animate for ..R.... with speed of 2`` () =
    let expected =[
        "..x....";
        "....x..";
        "......x";
        ".......";
      ] 
    let result=animate "..R...." 2
    Assert.Equal<Collections.Generic.IEnumerable<string>>(expected,result)

[<Fact>]
let ``Should animate RR..LRL with speed of 3`` ()=
    let expected=[
        "xx..xxx";
        ".x.xx..";
        "x.....x";
        ".......";
        ]
    let result=animate "RR..LRL" 3
    Assert.Equal<Collections.Generic.IEnumerable<string>>(expected,result)

[<Fact>]
let ``Should animate LRLR.LRLR with speed of 2`` () = 
    let expected=[
        "xxxx.xxxx";
        "x..x.x..x";
        ".x.x.x.x.";
        ".x.....x.";
        ".........";
    ]
    let result=animate "LRLR.LRLR" 2
    Assert.Equal<Collections.Generic.IEnumerable<string>>(expected,result)

[<Fact>]
let ``Should animate RLRLRLRLRL with speed of 10`` ()=
    let expected=[
      "xxxxxxxxxx";
      ".........."
    ]
    let result=animate "RLRLRLRLRL" 10
    Assert.Equal<Collections.Generic.IEnumerable<string>>(expected,result)

[<Fact>]
let ``Should LRRL.LR.LRR.R.LRRL. with speed of 1`` () =
    let expected=[
      "xxxx.xx.xxx.x.xxxx."; 
      "..xxx..x..xx.x..xx."; 
      ".x.xx.x.x..xx.xx.xx"; 
      "x.x.xx...x.xxxxx..x"; 
      ".x..xxx...x..xx.x.."; 
      "x..x..xx.x.xx.xx.x.";
      "..x....xx..xx..xx.x";
      ".x.....xxxx..x..xx.";
      "x.....x..xx...x..xx";
      ".....x..x.xx...x..x";
      "....x..x...xx...x..";
      "...x..x.....xx...x.";
      "..x..x.......xx...x";
      ".x..x.........xx...";
      "x..x...........xx..";
      "..x.............xx.";
      ".x...............xx";
      "x.................x";
      "...................";
    ]
    let result=animate "LRRL.LR.LRR.R.LRRL." 1
    Assert.Equal<Collections.Generic.IEnumerable<string>>(expected,result)

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
    Assert.Equal("..x.x.....",s)


