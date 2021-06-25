@check-disposed-after-feature
Feature: Singleton disposable objects after feature

As a tester,
I need my disposable singleton feature dependencies to be automatically disposed,
so that their resources are freed.

    Scenario: Singleton disposable feature dependencies are disposed after feature

    In order to check if a singleton disposable feature dependency actually is disposed
    after feature execution, we need to assert disposal in an AfterTestRun hook,
    hence the tag adorning this feature.

        Given I have injected a singleton disposable feature dependency in the binding class StepClassDisposableAfterFeature