function CreateDatabaseMySql($obj) {
	if ($config.debug) {
		Write-Host "* Create database (" $obj.databaseName ")" -f magenta 
	}	
	[void][system.reflection.Assembly]::LoadWithPartialName("MySql.Data") 
	$conn = New-Object MySql.Data.MySqlClient.MySqlConnection($obj.connStr)
	$conn.Open()
	$command = 'CREATE DATABASE `' + $obj.databaseName + '`'
	$cmd = New-Object MySql.Data.MySqlClient.MySqlCommand($command, $conn)
	$da = New-Object MySql.Data.MySqlClient.MySqlDataAdapter($cmd)
	$ds = New-Object System.Data.DataSet
	$da.Fill($ds)
}

function CreateDatabaseSql($obj) {
	if ($config.debug) {
		Write-Host "* Create database (" $obj.databaseName ")" -f magenta 
	}		
	$conn = New-Object Data.SqlClient.SqlConnection($obj.connStr)
	$conn.Open()
	$command = 'CREATE DATABASE ' + $obj.databaseName
	$cmd = New-Object Data.SqlClient.SqlCommand($command, $conn)
	$da = New-Object Data.SqlClient.SqlDataAdapter($cmd)
	$ds = New-Object System.Data.DataSet
	$da.Fill($ds)
}