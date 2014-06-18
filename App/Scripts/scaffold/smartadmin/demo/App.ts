﻿/// <reference path="../../../typings/angularjs/angular.d.ts"/>
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
                when('/scaffold/smartadmin/graph', {
                    templateUrl: '/scaffold/smartadmin/partials/Graph',
                    controller: 'GraphController'
                }).
                otherwise({
                    templateUrl: '/scaffold/smartadmin/partials/NotFound',
                    controller: 'StaticController'
                });
        }]).run(function () {
            console.log("run");
        });
} 