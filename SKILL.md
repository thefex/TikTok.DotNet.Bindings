# TikTok.DotNet.Bindings

This repo is not affiliated with TikTok by any means — these are .NET iOS bindings.

## Copilot prompt: create/update TikTok Business SDK .NET iOS bindings (local run)

Use this workflow for:
https://github.com/tiktok/tiktok-business-ios-sdk

> You will run locally. Use GitHub Releases `.xcframework` (not CocoaPods), then use `dotnet tool run sharpie`.

### Prerequisites

- Xcode installed
- `dotnet tool run sharpie` available (`dotnet tool install -g sharpie.bind.tool` if missing)
- .NET SDK with iOS workload installed (`dotnet workload install ios`)
- `curl` + `unzip`

---

### 1) Download the upstream `.xcframework` from Releases

```bash
mkdir -p ~/tmp/tiktokbuild && cd ~/tmp/tiktokbuild

# Check the latest available tag at:
# https://github.com/tiktok/tiktok-business-ios-sdk/releases
# Set SDK_VERSION to the latest tag before proceeding.
SDK_VERSION=1.6.1
curl -L -o TikTokBusinessSDK-${SDK_VERSION}.xcframework.zip \
  https://github.com/tiktok/tiktok-business-ios-sdk/releases/download/${SDK_VERSION}/TikTokBusinessSDK-${SDK_VERSION}.xcframework.zip
unzip -o TikTokBusinessSDK-${SDK_VERSION}.xcframework.zip
```

Expected output folder:

- `TikTokBusinessSDK.xcframework`

---

### 2) Set up a .NET iOS binding project

If the binding project does not already exist, scaffold it:

```bash
REPO="/path/to/TikTok.DotNet.Bindings"

dotnet new iosbinding -n TikTokBusinessSDK -o "$REPO/TikTokBusinessSDK"
```

Copy native artifacts into the binding project:

```bash
cp -R ~/tmp/tiktokbuild/TikTokBusinessSDK.xcframework "$REPO/TikTokBusinessSDK/"
# Workaround: Releases provide one xcframework artifact, and this project expects both
# release and -debug folder names, so the same artifact is copied under both names.
cp -R ~/tmp/tiktokbuild/TikTokBusinessSDK.xcframework "$REPO/TikTokBusinessSDK/TikTokBusinessSDK-debug.xcframework"
```

Ensure the `.csproj` contains a native reference:

```xml
<ItemGroup>
  <NativeReference Include="TikTokBusinessSDK.xcframework">
    <Kind>Framework</Kind>
    <SmartLink>False</SmartLink>
    <ForceLoad>False</ForceLoad>
  </NativeReference>
</ItemGroup>
```

---

### 3) Snapshot existing bindings before regenerating

**Always snapshot before running Sharpie on an update.** This lets you diff new output against prior bindings to catch regressions or missed cleanups.

```bash
REPO="/path/to/TikTok.DotNet.Bindings"

cp "$REPO/TikTokBusinessSDK/ApiDefinition.cs"   ~/tmp/tiktokbuild/ApiDefinition.old.cs
cp "$REPO/TikTokBusinessSDK/StructsAndEnums.cs" ~/tmp/tiktokbuild/StructsAndEnums.old.cs
```

---

### 4) Generate bindings with Sharpie (from xcframework headers)

Get the available iOS SDK name first:

```bash
xcodebuild -showsdks | grep iphoneos
# e.g. iphoneos26.2
```

Then run Sharpie against the `ios-arm64` framework slice (the umbrella header, not a Swift header):

```bash
cd ~/tmp/tiktokbuild

dotnet tool run sharpie bind \
  --output TikTokBusinessSDK-bindings-new \
  --namespace TikTokBusinessSDK \
  --sdk iphoneos<VERSION> \
  --framework TikTokBusinessSDK.xcframework/ios-arm64/TikTokBusinessSDK.framework
```

---

### 5) Diff new output against snapshots

Run these diffs **before** merging into the project. Analyse the output and decide which changes are legitimate new API vs. Sharpie noise.

```bash
diff ~/tmp/tiktokbuild/ApiDefinition.old.cs \
     ~/tmp/tiktokbuild/TikTokBusinessSDK-bindings-new/ApiDefinition.cs

diff ~/tmp/tiktokbuild/StructsAndEnums.old.cs \
     ~/tmp/tiktokbuild/TikTokBusinessSDK-bindings-new/StructsAndEnums.cs
```

**Rules for reviewing the diff:**

- Lines removed from the old file that are NOT in the new Sharpie output → the ObjC symbol was removed upstream; safe to drop.
- Lines added in the new file → new upstream API; apply the same cleanup rules below before merging.
- If a diff shows a previously-cleaned member (e.g. a renamed method) reverting to a Sharpie default name → **do not accept that revert**; keep the cleaned name and apply the new body only.
- If an interface that existed before is completely absent in new output → Sharpie may have dropped it due to a parse issue; investigate the header before removing it from the binding.
- Enum member renames or additions → verify against the ObjC header in `ios-arm64/TikTokBusinessSDK.framework/Headers/` before accepting.

---

### 6) Merge Sharpie output into project files

Merge (do **not** blindly overwrite) the diff-reviewed changes into:

- `TikTokBusinessSDK/ApiDefinition.cs`
- `TikTokBusinessSDK/StructsAndEnums.cs`

Apply these cleanup rules to any newly added code:

- Remove all `[Verify (...)]` attributes — resolve each one manually:
  - `[Verify (ConstantsInterfaceAssociation)]` → remove the `[Verify]`; keep the `Constants` interface unless it only contains SDK-internal version number/string fields, in which case drop the whole block.
  - `[Verify (MethodToProperty)]` on a **static** method → convert to a method (not a property) to avoid collisions with instance members of the same name.
  - `[Verify (MethodToProperty)]` on an **instance** method → convert to a method unless it truly is a zero-arg getter with no side effects.
- Fix duplicate C# signatures caused by ObjC overloads with the same parameter types (e.g. two `void Foo(string, string)` overloads) → disambiguate by incorporating the ObjC label into the C# name (e.g. `FooWithType` / `FooWithId`).
- Fix static/instance member name collisions: if a `[Static]` member has the same C# name as an instance member, rename the static one (e.g. `IsDebugMode()` → `IsDebugModeEnabled()`).
- Merge split `_Swift_####` extension interfaces into their primary interface; remove `[Category]` on those extension blocks.
- Keep exactly one `[BaseType(...)]` per final interface.
- Replace nested `Action<Action<...>>` patterns with named delegate types.
- Enum members: use PascalCase (no `@` verbatim identifiers).
- Remove the `using <YourNamespace>;` self-reference at the top of `ApiDefinition.cs`.

---

### 7) Build and verify

```bash
REPO="/path/to/TikTok.DotNet.Bindings"

dotnet build "$REPO/TikTokBusinessSDK/TikTokBusinessSDK.csproj" -c Release
dotnet build "$REPO/TikTokBusinessSDK/TikTokBusinessSDK.csproj" -c Debug
```

Both must pass with **0 errors, 0 warnings** before committing.

---

### 8) Update version metadata

In `TikTokBusinessSDK.csproj`, update:

```xml
<Version>X.Y.Z</Version>  <!-- match the upstream SDK tag -->
```

---

### 9) Copilot execution checklist

- [ ] Check latest release tag at https://github.com/tiktok/tiktok-business-ios-sdk/releases
- [ ] Download `.xcframework` zip and unzip
- [ ] Snapshot existing `ApiDefinition.cs` + `StructsAndEnums.cs` to `*.old.cs`
- [ ] Ensure/create .NET iOS binding project (`dotnet new iosbinding` if missing)
- [ ] Copy `.xcframework` (release + debug) into binding project
- [ ] Run `dotnet tool run sharpie bind` on `ios-arm64` framework slice
- [ ] Diff new Sharpie output against snapshots; review every changed line
- [ ] Merge only legitimate changes; apply all cleanup rules to new code
- [ ] Build Release + Debug — must be 0 errors, 0 warnings
- [ ] Update `<Version>` in `.csproj` to match SDK tag
- [ ] Commit and cleanup `~/tmp/tiktokbuild`

---

### Cleanup

```bash
rm -rf ~/tmp/tiktokbuild
```
