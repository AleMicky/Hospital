APP_API=Hospital.Api
APP_INFRA=Hospital.Infrastructure

migration:
	dotnet ef migrations add $(name) \
	--project $(APP_INFRA) \
	--startup-project $(APP_API) \
	--output-dir Data/Migrations

update:
	dotnet ef database update \
	--project $(APP_INFRA) \
	--startup-project $(APP_API)

remove:
	dotnet ef migrations remove \
	--project $(APP_INFRA) \
	--startup-project $(APP_API)

list:
	dotnet ef migrations list \
	--project $(APP_INFRA) \
	--startup-project $(APP_API)

watch:
	dotnet watch run --project $(APP_API)

run:
	dotnet run --project $(APP_API)

build:
	dotnet build

clean:
	dotnet clean

restore:
	dotnet restore

docker-up:
	docker compose up -d

docker-down:
	docker compose down

docker-reset:
	docker compose down -v
	
admin:
	cd Hospital.Admin && pnpm dev