Framework '4.0'

properties {
	$configuration = 'Debug'
	$platform = 'Any CPU'
}

Task Default -Depends Build

Task Build {
	MSBuild	-t:Build -p:Configuration=$configuration
}

Task MigrateUp -Depends Build {
	$fluentmigrator = Get-Item "packages/FluentMigrator.Tools.*/tools/AnyCPU/40/Migrate.exe"
	$migrationsAssembly = Get-Item "src/Migrations/bin/$configuration/*Migrations.dll"
	Invoke-Expression "$fluentmigrator --provider=sqlserver --connectionString=Main --target=$migrationsAssembly"
}
