<#
    .SYNOPIS
        Write secrets to secrets file
    
    .DESCRIPTION
        Writes secrets to the secrets file in %APPDATA%\Roaming\Microsoft\UserSecrets\OutdoorShop.Catalog

    .PARAMETER cosmosDbEndpoint
        Endpoint for CosmosDb

    .PARAMETER cosmosDbKey
        Key for CosmosDb

    .EXAMPLE
        .\write-usersecrets -cosmoDbEndpoint "http://localhost:8080/" -cosmosDbKey "adfadsf==?"
        .\write-usersecrets -variables $variables
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true, Position = 1)]
    $variables
)

begin {

}
process {
    $template = @"
{
}
"@
    $pathToSecretsFolder = "$env:APPDATA\Microsoft\UserSecrets\OutdoorShop.Catalog"
    $pathToSecrets = "$pathToSecretsFolder\secrets.json"

    if (!(Test-Path $pathToSecretsFolder)) {
        New-Item -Path $pathToSecretsFolder -ItemType "Directory"
    }

    if (!(Test-Path $pathToSecrets)) {
        New-Item -Path $pathToSecrets -ItemType "File" -Value $template
    }

    $file = Get-Content $pathToSecrets | ConvertFrom-Json

    $accountDetails = @{ 
        "CosmosDbEndpoint" = $variables.cosmosDbEndpoint   
        "CosmosDbKey"      = $variables.cosmosDbKey
    } 

    $file | Add-Member -Name "AzureAccountDetails" -Value $accountDetails -MemberType NoteProperty -Force

    $file | ConvertTo-Json | Set-Content $pathToSecrets
}
end {

}
