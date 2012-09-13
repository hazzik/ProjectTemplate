Framework '4.0'

properties {
    $home = $psake.build_script_dir
	$configuration = 'Debug'
	$platform = 'Any CPU'
	$fluentmigrator = Get-Item "packages/FluentMigrator.Tools.*/tools/AnyCPU/40/Migrate.exe"
	$xunitconsole = Get-Item "packages/xunit.runners.*/tools/xunit.console.clr4.exe"
    $packageLocation = "$home/artifacts/WebPackage.zip"
    $buildTempDirectory = "$home/build"
    $tempPackageDirectory = "$buildTempDirectory/temp/" + [System.Guid]::NewGuid().ToString("N")
}

Task Default -Depends Build

Task Init {
    [IO.Directory]::CreateDirectory($packageLocation)
}

Task Build {
	MSBuild -t:Build -p:Configuration=$configuration
}

Task Package -Depends Build {
    MSBuild -t:Package -p:Configuration=$configuration -p:PackageLocation=$packageLocation -p:_PackageTempDir=$tempPackageDirectory src/Web/Web.csproj
    Remove-Item -Recurse -Force $buildTempDirectory
}

Task Test -Depends Build {
	$testsAssembly = Get-Item "src/*/bin/$configuration/*Tests.dll"
	$testsAssembly | ForEach-Object { Invoke-Expression "$xunitconsole $_" }
}

Task MigrateUp -Depends Build {
	$migrationsAssembly = Get-Item "src/Migrations/bin/$configuration/*Migrations.dll"
	$migrationsAssembly | ForEach-Object { Invoke-Expression "$fluentmigrator --provider=sqlserver --connectionString=Main --target=$_" }
}
