<#
    .SYNOPIS
        Deploys an Azure Environment for a Developer

    .DESCRIPTION
        Spins up an Azure Environment a a developer so that each developer can work in their own space.
        Requires the Azure CLI
        Authenticate with Azure by excuting az login before running this script

    .PARAMETER devId
        Identifier for the developer, typically a 3 initials work great

    .EXAMPLE
        .\deploy-dev -devid XYZ 
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true, Position = 1)]
    [string] $devId
)

begin {

}
process {
    [string] $group = "$devId-OutdoorShop".ToLower()
    [string] $storage = ($devId + "OutdoorShop").ToLower()
    [string] $cosmosDb = "$group-CosmosDb".ToLower()

    $variables = @{ }
  
    Write-Host "Deploying $group"
         
    #New-AzResourceGroup -Name $group -Location "South Central US" 
    az group create --location "South Central US" --name $group
    
    #$deployment = New-AzResourceGroupDeployment -Name "dev-deployment" `
    #    -ResourceGroupName $group `
    #    -TemplateFile "azure-template.json" `
    #    -cosmosDbAccountName $cosmosDb `
    #    -storageAccountName $storage 

    $parameters = '{"cosmosDbAccountName": {"value": "' + $cosmosDb + '"}, "storageAccountName": {"value":"' + $storage + '"}}' | ConvertTo-Json

    $deployment = az group deployment create `
        --resource-group $group `
        --template-file ".\azure-template.json" `
        --parameters $parameters `
        --query properties.outputs | ConvertFrom-Json

    if ($deployment) {
        $variables.Add("storageName", $deployment.storageName.value)
        $variables.Add("storageUri", $deployment.storageUri.value)
        $variables.Add("cosmosDbEndpoint", $deployment.cosmosDbEndpoint.value)
        $variables.Add("cosmosDbKey", $deployment.cosmosDbKey.value)
    }
    else {
        Write-Host "Missing Deployment Outputs!"
    }    
   
    foreach ($key in $variables.Keys) {
        Write-Host "${key}: $($variables.Item($key))"
    }

    $write = $PSScriptRoot + "\write-usersecrets.ps1"
    &$write -variables $variables
}
end {

}
