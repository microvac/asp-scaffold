/// <reference path="../../typings/angularjs/angular.d.ts"/>
/// <reference path="../Models.ts"/>
/// <reference path="../App.ts"/>

/**
 * The main controller for the app. The controller:
 * - retrieves and persists the model via the todoStorage service
 * - exposes the model to the template and provides event handlers
 */


interface Window { todos: any }
module App.Todo {
    import app = App.app;
    import Todo = App.Models.Todo;

    app.controller("TodoCtrl", function ($scope) {
        Todo.GetAll().done(todos => {
            $scope.$apply(() => {
                window.todos = todos;
                $scope.todos = todos;
            });
        });
    });
}