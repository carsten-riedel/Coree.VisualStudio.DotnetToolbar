 param (
    [string]$SolutionDirectory = "default"
 )

function Remove-DirectorySafely {
    param (
        [string]$dirPath
    )

    if (Test-Path $dirPath -PathType Container) {
        # First remove all the files
        Get-ChildItem -Path $dirPath -Recurse -File | ForEach-Object {
            try {
                Remove-Item $_.FullName -Force
                Write-Host ("Removed file: " + $_.FullName)
            }
            catch {
                Write-Host ("Failed to remove file: " + $_.FullName)
            }
        }

        # Then remove all empty directories
        Get-ChildItem -Path $dirPath -Recurse -Directory | Sort-Object FullName -Descending | ForEach-Object {
            try {
                Remove-Item $_.FullName -Force
                Write-Host ("Removed directory: " + $_.FullName)
            }
            catch {
                Write-Host ("Failed to remove directory: " + $_.FullName)
            }
        }
        
        # Finally, attempt to remove the root directory itself
        try {
            if (Test-Path $dirPath -PathType Container) {
                Remove-Item -Path $dirPath -Force
                Write-Host ("Removed root directory: " + $dirPath)
            }
        }
        catch {
            Write-Host ("Failed to remove root directory: " + $dirPath)
        }
    }
    else {
        Write-Host "Either the directory does not exist, or the path is not a directory: $dirPath"
    }
}

 function Find-TopLevelDirectory {
    param (
        [string]$rootPath,
        [string]$directoryName
    )
  
    $directories = Get-ChildItem -Path $rootPath -Directory -Name

    foreach ($dir in $directories) {
        if ($dir -eq $directoryName) {
            return (Join-Path $rootPath $dir)
        }
    }
  
    return "Directory not found"
}

$bindir = Find-TopLevelDirectory -rootPath "$SolutionDirectory" -directoryName "bin"
$objdir = Find-TopLevelDirectory -rootPath "$SolutionDirectory" -directoryName "obj"

Remove-DirectorySafely -dirPath $bindir
Remove-DirectorySafely -dirPath $objdir




