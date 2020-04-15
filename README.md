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
