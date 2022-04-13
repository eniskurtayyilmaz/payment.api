@ECHO OFF 


ECHO Please wait... Checking system information.

REM cd C:\Users\mypc_YILM248\source\repos\payment.api

dir



dotnet sonarscanner begin /k:"PaymentAPI" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="82c68aba2d00a6612e0a6a3d7057143ddd119471"

dotnet build

dotnet sonarscanner end /d:sonar.login="82c68aba2d00a6612e0a6a3d7057143ddd119471"


ECHO DONE

start chrome http://localhost:9000/
