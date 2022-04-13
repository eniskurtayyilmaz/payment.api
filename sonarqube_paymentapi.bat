@ECHO OFF 


ECHO Please wait... Checking system information.

REM cd C:\Users\mypc_YILM248\source\repos\payment.api

dir



dotnet sonarscanner begin /k:"PaymentAPI" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="88f786c9b0e33ac32b1fc66974811e112c4cdf00"  /d:sonar.cs.dotcover.reportsPaths=dotCover.Output.html

dotnet build –no-incremental

dotnet dotcover test --dcReportType=HTML

dotnet sonarscanner end /d:sonar.login="88f786c9b0e33ac32b1fc66974811e112c4cdf00"


ECHO DONE

start chrome http://localhost:9000/
