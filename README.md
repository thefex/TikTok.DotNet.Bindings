# TikTok.DotNet.Bindings

This repo is not affiliated with TikTok by any means — these are .NET iOS bindings.

## Copilot prompt: create/update TikTok Business SDK .NET iOS bindings (local run)

Use this workflow for:
https://github.com/tiktok/tiktok-business-ios-sdk

> You will run locally. Use GitHub Releases `.xcframework` (not CocoaPods), then use Sharpie.

### Prerequisites

- Xcode installed
- Objective Sharpie installed (`sharpie` in `PATH`)
- .NET SDK with iOS workload installed (`dotnet workload install ios`)
- `curl` + `unzip`

---

### 1) Download the upstream `.xcframework` from Releases

```bash
mkdir -p ~/tmp/tiktokbuild && cd ~/tmp/tiktokbuild

# Pick a release tag from:
# https://github.com/tiktok/tiktok-business-ios-sdk/releases
# Verify the latest available tag before setting SDK_VERSION.
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
REPO="${PWD}" # run from repo root
cd "$REPO"

dotnet new iosbinding -n TikTokBusinessSDK -o TikTokBusinessSDK
```

Copy native artifacts into the binding project:

```bash
cp -R ~/tmp/tiktokbuild/TikTokBusinessSDK.xcframework "$REPO/TikTokBusinessSDK/"
# This project keeps both names; debug reuses the same upstream release artifact.
cp -R ~/tmp/tiktokbuild/TikTokBusinessSDK.xcframework "$REPO/TikTokBusinessSDK/TikTokBusinessSDK-debug.xcframework"
```

Ensure the `.csproj` contains a native reference (adjust path if needed):

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

### 3) Generate bindings with Sharpie (from xcframework headers)

```bash
cd ~/tmp/tiktokbuild

sharpie bind \
  -output TikTokBusinessSDK-bindings-new \
  -namespace TikTokBusinessSDK \
  -sdk iphoneos \
  TikTokBusinessSDK.xcframework/ios-arm64/TikTokBusinessSDK.framework/Headers/TikTokBusinessSDK-Swift.h
```

---

### 4) Merge Sharpie output into project files

Merge generated files into:

- `TikTokBusinessSDK/ApiDefinitions.cs`
- `TikTokBusinessSDK/StructsAndEnums.cs`

Do **not** blindly overwrite manual edits.

Also apply standard Sharpie cleanup:

- Merge split `_Swift_####` extension interfaces into primary interfaces
- Remove redundant `[Category]` on extension blocks before merging
- Keep exactly one `[BaseType(...)]` per final interface
- Remove `partial interface Constants` blocks that reference `__Internal`
- Replace nested `Action<Action<...>>` patterns with named delegates
- Rename duplicate C# signatures introduced by extension merging

---

### 5) Build and verify

```bash
REPO="${PWD}" # run from repo root
cd "$REPO"

dotnet build TikTokBusinessSDK/TikTokBusinessSDK.csproj -c Release
dotnet build TikTokBusinessSDK/TikTokBusinessSDK.csproj -c Debug
```

---

### 6) Copilot execution plan (checklist)

- [ ] Download target TikTok SDK `.xcframework` from GitHub Releases
- [ ] Ensure/create .NET iOS binding project (`dotnet new iosbinding` if missing)
- [ ] Copy `.xcframework` into binding project and verify `.csproj` `NativeReference`
- [ ] Run Sharpie on device header from `ios-arm64` slice
- [ ] Diff and merge generated APIs/enums into binding files
- [ ] Apply Sharpie cleanup fixes (`_Swift_####`, constants, delegates, duplicates)
- [ ] Build Debug/Release and fix binding compile issues
- [ ] Update generated snapshots/version metadata (if used in repo)

---

### Cleanup

```bash
rm -rf ~/tmp/tiktokbuild
```
