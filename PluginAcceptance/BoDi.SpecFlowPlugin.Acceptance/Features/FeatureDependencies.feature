Feature: Feature Dependencies

	As a tester,
	I need to inject feature dependencies in my scenarios step definition classes,
	so that I can access them.

	Scenario: Injected transient feature dependencies are different in all SpecFlow bindings

		A transient dependency is an object that is created anew each time it is 
		being resolved. Changes in such objects made in a given SpecFlow binding
		should not be seen in another SpecFlow binding.

		Given I have injected TransientFeatureDependency in the binding class StepClass1
		And the property MyProp of TransientFeatureDependency has value 'value1' in StepClass1
		And I have injected TransientFeatureDependency in the binding class StepClass2
		When I set property MyProp of TransientFeatureDependency to 'value2' in StepClass2
		Then the property MyProp of TransientFeatureDependency has value 'value1' in StepClass1

	Scenario: Injected singleton feature dependencies are the same in all SpecFlow bindings

		Given I have injected SingletonFeatureDependency in the binding class StepClass1
		And I have injected SingletonFeatureDependency in the binding class StepClass2
		When I set property MyProp of SingletonFeatureDependency to 'value' in StepClass1
		Then the property MyProp of SingletonFeatureDependency has value 'value' in StepClass2
