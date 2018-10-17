Add-WindowsFeature Web-Server
Set-Content -path "c:\inetpub\wwwrot\default.htm" -Value "Hello World from host $($env:computername) !"