/// <reference path="../../typings/angularjs/angular.d.ts"/>
/// <reference path="../../scaffold/Models.ts"/>
/// <reference path="../App.ts"/>
var App;
(function (App) {
    /**
    * The main controller for the app. The controller:
    * - retrieves and persists the model via the todoStorage service
    * - exposes the model to the template and provides event handlers
    */
    (function (_Todo) {
        var app = App.app;
        var Todo = App.Models.Todo;
        var Anu = App.Models.Anu;

        var anu = new Anu();
        anu.Dua = 3;
        anu.Satu = 1;
        anu.PostCount().done(function (hasil) {
            anu.Satu = hasil;
            anu.Dua = hasil;
        });

        app.controller("TodoCtrl", function ($scope) {
            Todo.GetAll().done(function (todos) {
                $scope.$apply(function () {
                    $scope.todos = todos;
                });
            });
        });
    })(App.Todo || (App.Todo = {}));
    var Todo = App.Todo;
})(App || (App = {}));
//# sourceMappingURL=TodoCtrl.js.map
