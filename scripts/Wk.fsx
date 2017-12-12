#r "../src/WkTypeProvider/bin/Debug/netstandard2.0/WkTypeProvider.dll"

type Sample = Wk.TypeProvider.MyType
Sample.MyProperty |> printfn "%A"

