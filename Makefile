SHELL 							:= bash
BIN_DIRS						:= $(shell find . -name 'bin')
OBJ_DIRS						:= $(shell find . -name 'obj')
BUILD_CONFIG					:= Release

VERSION = $(shell minver -v w | tail -1)

## Default target to execute
.DEFAULT_GOAL 					:= continuous_integration

.PHONY: clean
clean:
	@echo "Cleaning directories"
	@rm -rf $(BIN_DIRS)
	@rm -rf $(OBJ_DIRS)

.PHONY: setup
setup:
	@echo "Creating artifact directory"	
	@mkdir $(ARTIFACT_DIR)

	@echo "Installing 'minver-cli'"
	@dotnet tool install --global --verbosity quiet minver-cli
	@echo "Installing dependencies completed"

.PHONY: build
build:
	@echo "Building Tax Calculator Project v$(VERSION)"
	@dotnet build --configuration $(BUILD_CONFIG)

.PHONY: test
test:
	@dotnet test

continuous_integration: clean setup build test