
var name = "WkTypeProvider";
var id3 = "ID3TypeProvider";

Task("Build").Does(() => {
    MSBuild($"{id3}/{id3}.fsproj");
    MSBuild($"{name}/{name}.fsproj");
});

Task("Clean").Does(() => {
    MSBuild($"{name}/{name}.fsproj", config => {
        config.Targets.Add("Rebuild");
    });
});

var target = Argument("target", "default");
RunTarget(target);