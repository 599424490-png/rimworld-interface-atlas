param(
 [Parameter(Mandatory=$true)][string]$AssemblyPath,
 [string]$OutputDirectory=(Join-Path $PSScriptRoot '../local-index')
)
$ErrorActionPreference='Stop'
$assembly=(Resolve-Path -LiteralPath $AssemblyPath).Path
$pre=(Get-FileHash -LiteralPath $assembly -Algorithm SHA256).Hash.ToLowerInvariant()
$refs=@(Get-ChildItem -LiteralPath (Join-Path $PSHOME 'ref') -File -Filter '*.dll'|ForEach-Object FullName)
if(!$refs.Count){throw 'PowerShell 7 with reference assemblies is required'}
Add-Type -Path (Join-Path $PSScriptRoot 'MetadataIndex.cs') -ReferencedAssemblies $refs
[void](New-Item -ItemType Directory -Path $OutputDirectory -Force)
$directory=(Resolve-Path -LiteralPath $OutputDirectory).Path
$index=Join-Path $directory 'signatures.jsonl'
$identity=[MetadataIndex]::Generate($assembly,$index)|ConvertFrom-Json
$post=(Get-FileHash -LiteralPath $assembly -Algorithm SHA256).Hash.ToLowerInvariant()
if($pre -cne $post){throw 'Assembly changed during read'}
$manifest=[ordered]@{schema='local-metadata-index-v1';assemblySha256=$pre;identity=$identity;indexFile='signatures.jsonl';indexSha256=(Get-FileHash -LiteralPath $index).Hash.ToLowerInvariant();methodBodiesRead=0;targetAssemblyExecuted=$false;evidence='Metadata declarations only; independently review changed versions and runtime behavior'}
$manifest|ConvertTo-Json -Depth 30|Set-Content -LiteralPath (Join-Path $directory 'manifest.json') -Encoding utf8
$manifest|ConvertTo-Json -Depth 30
