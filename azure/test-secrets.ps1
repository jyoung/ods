$variables = @{ }
$variables.Add("storageName", "Storage Name")
$variables.Add("storageUri", "Storage Uri")
$variables.Add("cosmosDbEndpoint", "CosmosDb Endpoint")
$variables.Add("cosmosDbKey", "CosmosDb Key")

$write = $PSScriptRoot + "\write-usersecrets.ps1"
& $write -variables $variables