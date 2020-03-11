ForEach ($folder in (Get-ChildItem -Path .\ -Directory -Filter *.Tests)) { 
    dotnet test $folder.FullName --test-adapter-path:. --logger:"xunit;LogFilePath=test_results.trx"
}