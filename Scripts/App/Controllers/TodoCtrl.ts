/// <reference path="../../../Scaffold/Scripts/typings/angularjs/angular.d.ts"/>
/// <reference path="../../Models.ts"/>
/// <reference path="../App.ts"/>

/**
 * The main controller for the app. The controller:
 * - retrieves and persists the model via the todoStorage service
 * - exposes the model to the template and provides event handlers
 */


module App.Todo {
    import app = App.app;
    import Todo = App.Models.Todo;
    import Anu = App.Models.Anu;

    var anu = new Anu();
    anu.Dua = 3;
    anu.Satu = 1;
    anu.PostCount().done((hasil) => {
        anu.Satu = hasil;
        anu.Dua = hasil;
    })

    app.controller("TodoCtrl", function ($scope) {
        Todo.GetAll().done(todos => {
            $scope.$apply(() => {
                $scope.todos = todos;
            });
        });
    });
}