## Steps performed

- STEP1 : `npx react-native init TestRNWRelease --version ^0.61.5`
- STEP2 : `yarn add rnpm-plugin-windows --dev`
- STEP3 : `npx react-native windows`
- STEP4 :
  - Open `node_modules/react-native-windows/PropertySheets/React.Cpp.props`
  - Replace `<USE_V8 Condition="'$(USE_V8)' == '' AND '$(Platform)' != 'ARM' AND '$(Platform)' != 'ARM64'">true</USE_V8>`
    with `<USE_V8>false</USE_V8>`
  - Test
    - Debug x64 OK (VS throws a few exceptions, but continue anyway)
    - Realease x64 OK
