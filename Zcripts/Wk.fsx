#r "../WkTypeProvider/bin/Debug/WkTypeProvider.dll"

type Sample = Wk.TypeProviders.WkProvider< @"/Users/wk/Source/ID3TypeProvider/Resource/sample.txt" >
let kk = Sample()

kk |> printfn "%A"
kk.FieldA |> printfn "%A"
kk.FieldB |> printfn "%A"
kk.FieldC |> printfn "%A"
