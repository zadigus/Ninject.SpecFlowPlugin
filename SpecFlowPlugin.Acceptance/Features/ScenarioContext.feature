Feature: Scenario Context
	
	As a tester,
	I need to have the same scenario context shared across my specflow bindings,
	so that I can share scenario data between scenario steps.

	Scenario: The scenario context is the same in all SpecFlow bindings

		On the one hand, a given scenario might involve multiple step definition classes (SpecFlow bindings).
		On the other hand, a given scenario has one, and only one, scenario context. 
		We need to ensure that the scenario context be the same within a given scenario, 
		whatever step definition class is using it.

		Given I have injected the scenario context in the binding class StepClass1
		And the scenario context has no value corresponding to key 'MyKey' in StepClass1
		And I have injected the scenario context in the binding class StepClass2
		When I associate the value 'MyValue' to key 'MyKey' of the scenario context in StepClass1
		Then the scenario context delivers the value 'MyValue' for key 'MyKey' in StepClass2

	Scenario Outline: The scenario context is re-initialized before each scenario

		The structure of this Gherkin scenario is not really standard. That's because we 
		need to test special SpecFlow behavior and as such the usually recommended 
		Given, When, Then structure does not apply here. Also, the examples are only there
		to run more than one scenario with the same steps.

		Here we check that on each scenario run, the scenario context gets wiped off.

		Given I have injected the scenario context in the binding class StepClass1
		Then the scenario context has no value corresponding to key 'MyKey' in StepClass1
		When I associate the value 'MyValue' to key 'MyKey' of the scenario context in StepClass1

		Examples:

			| run   |
			| run-1 |			
			| run-2 |