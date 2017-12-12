namespace My

open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open System.IO
open System.Collections
open System.Collections.Generic

type FrameDictionary = System.Collections.Generic.Dictionary<string, string>

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
        typeDef
    do
        wkProviderType.DefineStaticParameters( [ ProvidedStaticParameter("fileName", typeof<string>) ], init)  
    do
        this.AddNamespace(ns, [ wkProviderType ])

[<assembly:TypeProviderAssembly>]
do ()