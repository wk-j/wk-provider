#r "../ID3TypeProvider/bin/Debug/ID3TypeProvider.dll"
#r "../WkTypeProvider/obj/Debug/WkTypeProvider.dll"

type AudioSample = DidacticCode.TypeProviders.ID3Provider< @"/Users/wk/Source/ID3TypeProvider/Resource/sample02.mp3" >

let audio = AudioSample()
audio.ContentType |> printfn "%A"
audio.Year        |> printfn "%A"
audio.Copyright   |> printfn "%A"