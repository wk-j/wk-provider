namespace DidacticCode.ID3

open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open System.IO
open System.Collections
open System.Collections.Generic

type FrameDictionary = System.Collections.Generic.Dictionary<string, ID3Frame>

module X = 
    let readFile name = 
        File.ReadAllText name

[<TypeProvider>]
type WkProvider() as this = 
    inherit TypeProviderForNamespaces()

    let assembly = System.Reflection.Assembly.GetExecutingAssembly()
    let ns = "Wk.TypeProviders"
    let wkProviderType = ProvidedTypeDefinition(assembly, ns, "WkProvider", None)

    let init typeName ([|:? string as fileName|] : obj array) = 
        let typeDef = ProvidedTypeDefinition(assembly, ns, typeName, None)
        makeProvidedConstructor
            List.empty
            (fun [] -> <@@ fileName |> X.readFile @@>)
            // (fun [] -> <@@ fileName |> ID3Reader.readID3Frames @@>)
        |>! addXmlDocDelayed "Create wk from file."
        |> typeDef.AddMember

        let values = 
            dict [
                ("FieldA", "A")
                ("FieldB", "B")
                ("FieldC", "C")
            ]

        let makePropertyBody frame ([frames]) = <@@ "XXX" @@>

        values
        |> Seq.choose (fun f -> match f.Key with
                                | "FieldA" as frame ->
                                    "FieldA"
                                    |> makeProvidedProperty<string> (makePropertyBody frame)
                                    |>! addXmlDocDelayed "Get field A"
                                    |> Some
                                | "FieldC" as frame ->
                                    "FieldC"
                                    |> makeProvidedProperty<string> (makePropertyBody frame)
                                    |>! addXmlDocDelayed "Get field C"
                                    |> Some
                                | "FieldB" as frame ->
                                    "FieldB"
                                    |> makeProvidedProperty<string> (makePropertyBody frame)
                                    |>! addXmlDocDelayed "Get field B"
                                    |> Some
                                | _ -> None)
        |> Seq.toList
        |> typeDef.AddMembers

        typeDef
    do
        wkProviderType.DefineStaticParameters( [ ProvidedStaticParameter("fileName", typeof<string>) ], init)  
    do
        this.AddNamespace(ns, [ wkProviderType ])

[<assembly:TypeProviderAssembly>]
do ()