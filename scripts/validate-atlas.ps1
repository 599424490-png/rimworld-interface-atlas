$ErrorActionPreference='Stop'
$atlas=Get-Content -LiteralPath (Join-Path $PSScriptRoot '../atlas.json') -Raw -Encoding UTF8|ConvertFrom-Json -Depth 100
$ids=[Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
$domains=[Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
$atlas.domains|ForEach-Object{[void]$domains.Add($_)}
if($domains.Count -ne 23 -or $atlas.entries.Count -ne 193){throw 'Unexpected scope counts'}
foreach($e in $atlas.entries){
 if(!$ids.Add($e.id)){throw 'Duplicate entry ID'}
 if(!$domains.Contains($e.domain)){throw 'Unknown domain'}
 if(!$e.user_goal -or !$e.status){throw 'Missing navigation fields'}
 foreach($ref in $e.evidence_refs){if(!$atlas.sources.PSObject.Properties[$ref]){throw 'Missing evidence reference'}}
}
foreach($e in $atlas.entries){if($e.alias_of -and !$ids.Contains($e.alias_of)){throw 'Missing alias destination'}}
if($atlas.target.version -ne '1.6.9676.17735'){throw 'Unexpected baseline'}
[ordered]@{domains=$domains.Count;entries=$ids.Count;uniqueIDs=$true;sourceReferencesValid=$true;runtimeValidated=$false}|ConvertTo-Json
