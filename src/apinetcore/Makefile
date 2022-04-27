
add-migration-to-library: #make add-migration-to-library name=Initial
	@echo "$(CYAN_COLOR)==> Adding migration to library...$(NO_COLOR)"
	@dotnet ef migrations add  $(name) -c "Lanre.Context.Library.Infrastructure.Database.LibraryContext" -o "Infrastructure/Database/Migrations" -p "./Lanre.Context.Library" -s "./Lanre.Host"