Param(
    [switch]$Reset,
	[switch]$Debug
)

#SET THINGS UP
$imageName = "microsoft/mssql-server-linux:2017-CU13"
$dbContainer = "lexicon-db"
$flywaycopyContainer = "lexicon-flywaycopy"
$globalWait = 10
$dbname = "lexicon"

#INFORM THE USER OF THE PARAMETERS
$debugLog = "Parameters: "
$debugLog += "-Reset " + $Reset
$debugLog += " -Debug " + $Debug
Clear-Host
Write-Host $debugLog `n -f green

function ComposeUp {
	#START DATABASE CONTAINER(S) AND RUN DETACHED
	Write-Host "* Run container detatched (" $imageName ")" -f magenta
	docker-compose up -d $dbContainer

	Write-Host " ... wait for" $globalWait "seconds for" $dbContainer "to start"
	Start-Sleep $globalWait
}

if ($Reset) {
	#KILL AND DELETE
	Write-Host "* Kill and delete containers/volumes/network" -f magenta
	docker-compose down -v
	
	#SPIN UP
	ComposeUp

	#COPY NEW SQL FILES
	$executingScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
	$flywaySourceDirectory = $executingScriptDirectory -replace 'Docker-Compose','Flyway'
	Remove-Item -PATH flyway -Recurse -ErrorAction Ignore
	Copy-Item $flywaySourceDirectory -Destination $executingScriptDirectory"\flyway" -Recurse	
	
	#BUILD
	Write-Host "* Docker compose build" -f magenta
	docker-compose build --no-cache	
	
	#CREATE DATABASE
	Write-Host "* Create database (" $dbname ")" -f magenta 
	$connStr = "Server=localhost;Database=master;User Id=sa;Password=Password123;"
	$conn = New-Object Data.SqlClient.SqlConnection($connStr)
	$conn.Open()
	$createdatabase = 'CREATE DATABASE lexicon;'
	$cmd = New-Object Data.SqlClient.SqlCommand($createdatabase, $conn)
	$da = New-Object Data.SqlClient.SqlDataAdapter($cmd)
	$ds = New-Object System.Data.DataSet
	$da.Fill($ds)

	#FLYWAY BASELINE
	docker-compose up -d lexicon-baseline
	Write-Host " ... wait for" $globalWait "seconds for flyway baseline"
	Start-Sleep 5
	if ($Debug) {
		Write-Host "* logs from lexicon-baseline" -f magenta
		docker logs lexicon-baseline
	}
	
	#FLYWAY MIGRATE
	docker-compose up -d lexicon-migrate
	Write-Host " ... wait for" $globalWait "seconds for flyway migrate"
	Start-Sleep 5
	if ($Debug) {
		Write-Host "* logs from lexicon-migrate" -f magenta
		docker logs lexicon-migrate	
	}
	
	#CLEAN UP
	Remove-Item -PATH flyway -Recurse -ErrorAction Ignore	
} 
else 
{
	#SPIN UP
	ComposeUp
}

#DEBUG
if ($Debug) {
	Write-Host "* List all containers" -f magenta
	docker ps --all
	Write-Host "* List volume data" -f magenta
	docker run -it --rm -v docker-compose_lexicon-flyway:/vol busybox ls -l /vol
	docker run -it --rm -v docker-compose_lexicon-flyway:/vol busybox cat /vol/V1.0.5__lexicon_entry.sql
	docker logs lexicon-migrate
} 

Write-Host "* End of script" -f magenta