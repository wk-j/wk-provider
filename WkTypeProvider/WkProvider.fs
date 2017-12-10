namespace DidacticCode.ID3

#nowarn "0025"

open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes

type FrameDictionary = System.Collections.Generic.Dictionary<string, ID3Frame>

[<TypeProvider>]
type WkProvider() as this = 
    inherit TypeProviderForNamespaces()
    let asm = System.Reflection.Assembly.GetExecutingAssembly()
    let ns = "Wk.TypeProviders"
    let wkProviderType = ProvidedTypeDefinition(asm, ns, "WkProvider", None)
    
    let init typeName ([|:? string as fileName|] : obj array) = 
        let typeDef = ProvidedTypeDefinition(asm, ns, typeName, None)
        makeProvidedConstructor
            List.empty
            (fun [] -> <@@ fileName |> ID3Reader.readID3Frames @@>)
        |>! addXmlDocDelayed "Create wk from file"
        |> typeDef.AddMember
        typeDef

    do
        wkProviderType.DefineStaticParameters( [ ProvidedStaticParameter("fileName", typeof<string>) ], init)  
    do
        this.AddNamespace(ns, [ wkProviderType ])

[<assembly:TypeProviderAssembly>]
do ()