<#
    .SYNOPIS
        Deletes an Azure Environment for a Developer

    .DESCRIPTION
        Tears down an Azure Environment a a developer so that each developer can work in their own space

    .PARAMETER devId
        Identifier for the developer, typically a 3 initials work great

    .EXAMPLE
        .\delete-dev -devid XYZ
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true, Position = 1)]
    [string] $devId
)

begin {

}
process {
    [string] $group = "$devId-OutdoorShop"

    Write-Host "Deleting $group"
    
    az group delete -g $group
    #Remove-AzResourceGroup -Name $group
}
end {

}
