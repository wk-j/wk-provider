namespace DidacticCode.ID3

#nowarn "0025"

open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes

type FrameDictionary = System.Collections.Generic.Dictionary<string, ID3Frame>

[<TypeProvider>]
type ID3Provider() as this = 
  inherit TypeProviderForNamespaces()

  let assy = System.Reflection.Assembly.GetExecutingAssembly()
  let ns = "DidacticCode.TypeProviders"
  let id3ProviderType = ProvidedTypeDefinition(assy, ns, "ID3Provider", None)

  let makePropertyBody frame ([frames]) = <@@ ((%%frames:obj) :?> FrameDictionary).[frame].GetContent() @@>

  let instantiate typeName ([| :? string as fileName |]: obj array) =
    let ty = ProvidedTypeDefinition(assy, ns, typeName, None)
    makeProvidedConstructor
      List.empty
      (fun [] -> <@@ fileName |> ID3Reader.readID3Frames @@>)
    |>! addXmlDocDelayed "Creates a reader for the specified file."
    |> ty.AddMember

    fileName
    |> ID3Reader.readID3Frames
    |> Seq.choose (fun f -> match f.Key.ToUpperInvariant() with
                            | "APIC" as frame ->
                               "AttachedPicture"
                               |> makeProvidedProperty<AttachedPicture> (makePropertyBody frame)
                               |>! addXmlDocDelayed "Gets the picture attached to the file. Corresponds to the APIC frame."
                               |> Some
                            | "MCDI" as frame ->
                                "CdIdentifier"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the CD Identifier. Corresponds to the MCDI frame."
                                |> Some
                            | "POPM" as frame ->
                                "Popularimeter"
                                |> makeProvidedProperty<Popularimeter> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the Popularimeter data including play count and rating. Corresponds to the POPM frame."
                                |> Some
                            | "TALB" as frame ->
                                "AlbumTitle"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the album title. Corresponds to the TALB frame."
                                |> Some
                            | "TIT1" as frame ->
                                "ContentGroup"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the content group. Corresponds to the TIT1 frame."
                                |> Some
                            | "TIT2" as frame ->
                                "TrackTitle"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the track title. Corresponds to the TIT2 frame."
                                |> Some
                            | "TIT3" as frame ->
                                "TrackSubtitle"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the track subtitle. Corresponds to the TIT3 frame."
                                |> Some
                            | "TRCK" as frame ->
                                "TrackNumber"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the track number. Corresponds to the TRCK frame."
                                |> Some
                            | "TYER" as frame ->
                                "Year"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the year the track was released. Corresponds to the TYER frame."
                                |> Some
                            | "TPE1" as frame ->
                                "Performer"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the track performer's name. Corresponds to the TPE1 frame."
                                |> Some
                            | "TPE2" as frame ->
                                "Band"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the band name. Corresponds to the TPE2 frame."
                                |> Some
                            | "TPOS" as frame ->
                                "SetIdentifier"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the track's position within the set. Corresponds to the TPOS frame."
                                |> Some
                            | "TPUB" as frame ->
                                "Publisher"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the track publisher's name. Corresponds to the TPUB frame."
                                |> Some
                            | "TCOM" as frame ->
                                "Composer"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the track composer's name. Corresponds to the TCOM frame."
                                |> Some
                            | "TCON" as frame ->
                                "ContentType"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the track's content type. Corresponds to the TCON frame."
                                |> Some
                            | "TCOP" as frame ->
                                "Copyright"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the copyright information for the track. Corresponds to the TCOP frame."
                                |> Some
                            | "TLEN" as frame ->
                                "TrackLength"
                                |> makeProvidedProperty<string> (makePropertyBody frame)
                                |>! addXmlDocDelayed "Gets the length of the track. Corresponds to the TLEN frame."
                                |> Some
                            | _ -> None)
    |> Seq.toList
    |> ty.AddMembers

    ty

  do
    id3ProviderType.DefineStaticParameters(
      [ ProvidedStaticParameter("fileName", typeof<string>) ],
      instantiate)

  do
    this.AddNamespace(ns, [ id3ProviderType ])

[<assembly:TypeProviderAssembly>]
do ()