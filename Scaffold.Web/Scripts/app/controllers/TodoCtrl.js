/// <reference path="../../typings/angularjs/angular.d.ts"/>
/// <reference path="../Models.ts"/>
/// <reference path="../App.ts"/>
var App;
(function (App) {
    (function (_Todo) {
        var app = App.app;
        var Todo = App.Models.Todo;

        app.controller("TodoCtrl", function ($scope) {
            Todo.GetAll().done(function (todos) {
                $scope.$apply(function () {
                    window.todos = todos;
                    $scope.todos = todos;
                });
            });
        });
    })(App.Todo || (App.Todo = {}));
    var Todo = App.Todo;
})(App || (App = {}));
//# sourceMappingURL=TodoCtrl.js.map
