# User Requirement

1. Decide food menu for eating time in a day
2. Record user progress about nutrition
3. Record user weight progress
4. Create food menu plan for future

This app is going to be made with Quasar, Vue 3 UI Framework, with Asp Net Core 5.0 as backend

# Coding standards

## Front end

### 1. Naming Convention

- Use camelCase for local JS/TS variable
- Global variable name should begin with \_, example: \_context
- Use PascalCase for TS Interfaces and Classes
- Interfaces should begin with I, example: IPagination
- CSS class should use lowercase and dashes

### 2. File and Folder Naming and Structure

- Folder name should be lowercase with dash, example: base-component
- Vue component name should be PascalCase
- Javascript / Typescript module name should be lowercase with dash
- Vue component should be gathered in detail folder, example: components/ui/BaseButton.vue

### 3. Formatting and Indentation

- Should always use prettier with the config file inside the project

### 4. Commenting and Documenting

- Each function must have at least one-line description about its usage
- Create TODO comment for each tasks that will be done by the next commit
- Create description about a flow inside the source code if necessary

### 5. Testing

- E2E testing with Cypress
- Unit testing with JEST

## Back End

### 1. Naming Convention

- Use camelCase for local variable
- Private dependency variable name should begin with \_, example: \_context
- Use PascalCase for Classes, Namespaces, and Functions
- Interfaces should begin with I, example: IPagination

### 2. File and Folder Naming and Structure

- Folder name should use PascalCase
- Class file should use PascalCase
- Class file should match the class name inside the file

### 3. Formatting and Indentation

- Max print: 80 columns
- Tab width: 4 using spaces
- Curly brackets: new line
- Parentheses in functions: without spaces
- Parentheses not in function: with 1 space
- Function parameters: space after comma | new line if necessary
- Operators: use space for operand and operator
- Method calling: new line if necessary

### 4. Commenting and Documenting

- Each function must have at least one-line description about its usage
- Create TODO comment for each tasks that will be done by the next commit
- Create description about a flow inside the source code if necessary

### 5. Testing

- Unit testing will be done by using xunit

# Branching Strategies

For the branching strategies, this project uses the GitHub Flow branching
strategies.

Master branch is deployable branch.

Any other branch is development branch.

## Branch naming convention

- Feature branch: feature-\<feature-name\>
- Hotfix branch: hotfix-\<issue-number\>
