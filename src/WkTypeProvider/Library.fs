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

        let ctor = ProvidedConstructor([], invokeCode = fun args -> <@@ "My internal state" :> obj @@>)
        myType.AddMember(ctor)

        let ctor2 = ProvidedConstructor([ProvidedParameter("InnerState", typeof<string>)],
                        invokeCode = fun args -> <@@ (%%(args.[0]):string) :> obj @@> )
        myType.AddMember(ctor2)

        let innerState = ProvidedProperty("InnerState", typeof<string>,
                            getterCode = fun args -> <@@ (%%(args.[0]) :> obj) :?> string @@>)
        myType.AddMember(innerState)

        [myType]
    do
        this.AddNamespace(ns, createTypes())

[<assembly: TypeProviderAssembly>]
do ()