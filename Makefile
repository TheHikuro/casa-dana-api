.PHONY: init migrate setup

# Initialize the project
init:
	docker compose up --build -d
	dotnet restore
	dotnet build

# Run database migrations
migrate:
	dotnet ef migrations script

# Setup the project on a new laptop
setup: init migrate