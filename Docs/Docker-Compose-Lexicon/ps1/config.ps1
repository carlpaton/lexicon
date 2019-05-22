class ConfigurationData {
   [Boolean]$reset
   [Boolean]$debug
   [int]$globalWait
   [string]$containerDb
   [string]$containerWebMvc
   [string]$containerBaseline
   [string]$containerMigrate   
   [string]$databaseName
   [string]$connStr
   [string]$network   
}

$config = [ConfigurationData]::new()
$config.reset = $Reset
$config.debug = $Debug
$config.globalWait = 1
$config.containerDb = "lexicon-db"
$config.containerWebMvc = "lexicon-webmvc"
$config.containerBaseline = "lexicon-baseline"
$config.containerMigrate = "lexicon-migrate"
$config.databaseName = "lexicon"
$config.connStr = "Server=localhost;Database=master;User Id=sa;Password=Password123;"
$config.network = "docker-compose-lexicon_default"