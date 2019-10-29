$files=get-childitem -Path . -Recurse
foreach ($f in $files)
{
(Get-Content $f.PSPath) |
Foreach-Object {$_ -replace "\xEF\xBB\xBF", ""} |
Set-Content $f.PSPath
}
