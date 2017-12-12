#r "../WkTypeProvider/bin/Debug/WkTypeProvider.dll"

type Sample = Wk.TypeProviders.WkProvider< @"/Users/wk/Source/ID3TypeProvider/Resource/sample.txt" >
let sample = Sample()

sample |> printfn "%A"
sample.FieldA |> printfn "%A"
sample.FieldB |> printfn "%A"
sample.FieldC |> printfn "%A"