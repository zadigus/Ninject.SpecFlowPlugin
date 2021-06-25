Feature: Scenario Dependencies

	As a tester,
	I need to inject scenario dependencies in my scenarios step definition classes,
	so that I can access them.

	Scenario: Injected transient scenario dependencies are different in all SpecFlow bindings

		A transient dependency is an object that is created anew each time it is 
		being resolved. Changes in such objects made in a given SpecFlow binding
		should not be seen in another SpecFlow binding.

		Given I have injected TransientScenarioDependency in the binding class StepClass1
		And the property MyProp of TransientScenarioDependency has value 'value1' in StepClass1
		And I have injected TransientScenarioDependency in the binding class StepClass2
		When I set property MyProp of TransientScenarioDependency to 'value2' in StepClass2
		Then the property MyProp of TransientScenarioDependency has value 'value1' in StepClass1

	Scenario: SpecFlow bindings are Singletons

		Given I have injected StepClass3 in StepClass4
		And I have injected StepClass3 in StepClass5
		When I set property MyProp of StepClass3 to 'value' in StepClass4
		Then the property MyProp of StepClass3 has value 'value' in StepClass5

	Scenario: Injected singleton scenario dependencies are the same in all SpecFlow bindings

		Given I have injected SingletonScenarioDependency in the binding class StepClass1
		And I have injected SingletonScenarioDependency in the binding class StepClass2
		When I set property MyProp of SingletonScenarioDependency to 'value' in StepClass1
		Then the property MyProp of SingletonScenarioDependency has value 'value' in StepClass2
