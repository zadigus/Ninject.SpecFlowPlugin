@special-feature-value
Feature: Feature Context
	
	As a tester,
	I need to have the same feature context shared across my specflow bindings,
	so that I can share feature data between scenario steps.

	Scenario: The feature context is the same in all SpecFlow bindings

		We want to ensure that the feature context behave like a singleton.
		Wherever it gets injected within a given feature, it needs to remain 
		the same.

		Given I have injected the feature context in the binding class StepClass1
		And the feature context has no value corresponding to key 'MyKey' in StepClass1
		And I have injected the feature context in the binding class StepClass2
		When I associate the value 'MyValue' to key 'MyKey' of the feature context in StepClass1
		Then the feature context delivers the value 'MyValue' for key 'MyKey' in StepClass2

	Scenario Outline: The feature context is not re-initialized before each scenario

		The structure of this Gherkin scenario is not really standard. That's because we 
		need to test special SpecFlow behavior and as such the usually recommended 
		Given, When, Then structure does not apply here.

		Here we want to make sure that whatever gets changed in the feature context within 
		a scenario gets reflected in another scenario, i.e. the feature context is some kind 
		of singleton over an entire feature.

		Given I define the key '<step-class1>' in the feature context
		Then if the feature context contains the key '<step-class2>' then it should not contain the key 'MyKeySetInFeatureHook'
		And if the feature context does not contain the key '<step-class2>' then it should contain the key 'MyKeySetInFeatureHook'
		When I remove the key 'MyKeySetInFeatureHook' from the feature context in <step-class1>

		Examples:

			| step-class1 | step-class2 |
			| StepClass1  | StepClass2  |
			| StepClass2  | StepClass1  |