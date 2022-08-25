Remove-Item -Path ..\BackEnd\wwwroot\static -Force -Recurse
npm run build
xcopy build\ ..\BackEnd\wwwroot\ /s /y /q 