/// <reference path="../../../typings/angularjs/angular.d.ts"/>
module Smartadmin.Demo {
    export var app = angular.module('app', [
        'ngRoute',
        'angular-flot',
        'angular-sparkline',
    ]);

    app.config(['$routeProvider', '$locationProvider',
        function ($routeProvider, $locationProvider) {
            $locationProvider.html5Mode(true);
            $routeProvider.
                when('/scaffold/smartadmin/', {
                    templateUrl: '/scaffold/smartadmin/partials/Index',
                    controller: 'IndexController'
                }).
                when('/scaffold/smartadmin/flot', {
                    templateUrl: '/scaffold/smartadmin/partials/Flot',
                    controller: 'FlotController'
                }).
                otherwise({
                    redirectTo: '/scaffold/smartadmin/'
                });
        }]).run(function () {
            console.log("run");
        });
} 