
add-migration-to-library: #make add-migration-to-library name=Initial
	@echo "$(CYAN_COLOR)==> Adding migration to library...$(NO_COLOR)"
	@dotnet ef migrations add  $(name) -c "Lanre.Context.Library.Infrastructure.Database.LibraryContext" -o "Infrastructure/Database/Migrations" -p "./Lanre.Context.Library" -s "./Lanre.Host"

unit-tests-library: #make unit-tests-library
	@echo "$(CYAN_COLOR)==> Testing unit tests of library...$(NO_COLOR)"
	@docker-compose -f docker-compose.unit-tests.yml build library.unittests && docker-compose -f docker-compose.unit-tests.yml run library.unittests