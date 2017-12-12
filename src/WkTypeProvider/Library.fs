namespace WkTypeProvider

open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open System.Reflection

[<TypeProvider>]
type WkProvider (config: TypeProviderConfig) as this = 
    inherit TypeProviderForNamespaces(config)

    let ns = "Wk.TypeProvider"
    let asm = Assembly.GetExecutingAssembly()
    let createTypes() =
        let myType = ProvidedTypeDefinition(asm, ns, "MyType", Some typeof<obj>)
        let myProp = ProvidedProperty("MyProperty", typeof<string>, isStatic = true, getterCode = (fun args -> <@@ "Hello world" @@>))
        myType.AddMember(myProp)
        [myType]
    do
        this.AddNamespace(ns, createTypes())

[<assembly: TypeProviderAssembly>]
do ()