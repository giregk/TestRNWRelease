## Steps performed

- STEP 1 : `npx react-native init TestRNWRelease --version ^0.61.5`
- STEP 2 : `yarn add rnpm-plugin-windows --dev`
- STEP 3 : `npx react-native windows`
- STEP 4 :
  - Open `node_modules/react-native-windows/PropertySheets/React.Cpp.props`
  - Replace `<USE_V8 Condition="'$(USE_V8)' == '' AND '$(Platform)' != 'ARM' AND '$(Platform)' != 'ARM64'">true</USE_V8>`
    with `<USE_V8>false</USE_V8>`
  - Test
    - Debug x64 OK (VS throws a few exceptions, but continue anyway)
    - Realease x64 OK
- STEP 5 :
  - Project > Publish > Create App Package (choose x64 only)
    - (I'm not committing the store association changes)
  - At the end of the build, launch Windows App Certification Kit - It fails Supported API test
    with
    ```
    API __CxxFrameHandler4 in vcruntime140_1_app.dll is not supported for this application type. TestRNWRelease.exe calls this API.
    ```
    which is OK since it does not occur in the Store validation
- STEP 6 :
  - Right click solution > Add > New Project
  - Select `Windows Runtime Component (Universal Windows) C#`
  - Name it CustomNativeModule
  - Change Minimum version to `Windows 10 Creators Update (10.0; Build 15063)`
  - Right click on the CustomNativeModule project > Add > Reference
    - Project tab : select Microsoft.ReactNative
    - Shared Project tab : select Microsoft.ReactNative.SharedManaged
  - Expand the References of CustomNativeModule, right click the Microsoft.ReactNative library, choose Properties. Set Copy Local to false
  - Expose Device Model API
  - Add reference to CustomNativeModule in TestRNWRelease
  - Test
    - Debug x64 => OK
    - Release x64 => OK
- STEP 7 :
  - Project > Publish > Create App Package (choose x64 only)
  - Windows App Certification Kit fails the Supported API test
  ```
  API _XcptFilter in msvcrt.dll is not supported for this application type. clrcompression.dll calls this API.
  API __C_specific_handler in msvcrt.dll is not supported for this application type. clrcompression.dll calls this API.
  API _amsg_exit in msvcrt.dll is not supported for this application type. clrcompression.dll calls this API.
  API _initterm in msvcrt.dll is not supported for this application type. clrcompression.dll calls this API.
  API free in msvcrt.dll is not supported for this application type. clrcompression.dll calls this API.
  API malloc in msvcrt.dll is not supported for this application type. clrcompression.dll calls this API.
  API memcpy in msvcrt.dll is not supported for this application type. clrcompression.dll calls this API.
  API memset in msvcrt.dll is not supported for this application type. clrcompression.dll calls this API.
  API __CxxFrameHandler4 in vcruntime140_1_app.dll is not supported for this application type. TestRNWRelease.exe calls this API.
  API ExecuteAssembly in uwphost.dll is not supported for this application type. UWPShim.exe calls this API.
  API DllGetActivationFactory in uwphost.dll is not supported for this application type. UWPShim.exe has an export that forwards to this API.
  ```
  - it also fails the Windows security features test
  ```
  File C:\Program Files\WindowsApps\17704UpSignOn.UpSignOn_1.0.1.0_x64__a7xkq6qkvjapw\clrcompression.dll has failed the AppContainerCheck check.
  File C:\Program Files\WindowsApps\17704UpSignOn.UpSignOn_1.0.1.0_x64__a7xkq6qkvjapw\clrjit.dll has failed the AppContainerCheck check.
  ```
