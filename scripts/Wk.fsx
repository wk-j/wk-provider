#r "../src/WkTypeProvider/bin/Debug/netstandard2.0/WkTypeProvider.dll"

module A = 
    let sample = Wk.TypeProvider.MyType()
    sample.InnerState |> printfn "%A"

module B = 
    let sample = Wk.TypeProvider.MyType("My type")
    sample.InnerState |> printfn "%A"


