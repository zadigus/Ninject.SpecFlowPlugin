Feature: Transient disposable feature objects disposed after feature

As a tester,
I need my transient disposable feature dependencies to be automatically disposed,
so that their resources are freed.

    Scenario: Transient disposable feature dependencies are disposed after feature

    In order to check if a transient disposable feature dependency actually is disposed
    after feature execution, we need to assert disposal in an AfterTestRun hook.
	
	Transient disposable feature dependencies are not disposed after feature execution
    when we use BoDi as the DI framework instead of Ninject.

        Given I have injected a transient disposable feature dependency in the binding class StepClassDisposableAfterFeature