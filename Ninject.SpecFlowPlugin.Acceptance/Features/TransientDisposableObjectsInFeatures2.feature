Feature: Transient disposable feature objects disposed after feature 2

As a tester,
I need my transient disposable feature dependencies to be automatically disposed,
so that their resources are freed.

We need to make sure that transient disposable feature dependencies are disposed after 
each feature. Because we have no way to determine in what order the features are going 
to be executed, we need write this feature twice and write a hacky scenario.
	
    Scenario: Transient disposable feature dependencies 1 are disposed after feature

	Transient disposable feature dependencies are not disposed after feature execution
    when we use BoDi as the DI framework instead of Ninject.

	    Given I have injected TransientDisposableFeatureDependency2 in the binding class StepClassDisposableAfterFeature
	    Then TransientDisposableFeatureDependency1 has been disposed if the previous feature had to dispose it