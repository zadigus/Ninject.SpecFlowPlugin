Feature: Singleton disposable objects after feature 2

As a tester,
I need my disposable singleton feature dependencies to be automatically disposed,
so that their resources are freed.
    
We need to make sure that singleton disposable feature dependencies are disposed after 
each feature. Because we have no way to determine in what order the features are going 
to be executed, we need write this feature twice and write a hacky scenario.

    Scenario: Singleton disposable feature dependencies 1 are disposed after feature

    In order to check if a singleton disposable feature dependency actually is disposed
    after feature execution, we need to assert disposal  on test thread context.

        Given I have injected SingletonDisposableFeatureDependency2 in the binding class StepClassDisposableAfterFeature
        Then SingletonDisposableFeatureDependency1 has been disposed if the previous feature had to dispose it