var Smartadmin;
(function (Smartadmin) {
    /// <reference path="../../../typings/angularjs/angular.d.ts"/>
    (function (Demo) {
        Demo.app = angular.module('app', [
            'ngRoute',
            'angular-flot',
            'angular-sparkline'
        ]);

        Demo.app.config([
            '$routeProvider', '$locationProvider',
            function ($routeProvider, $locationProvider) {
                $locationProvider.html5Mode(true);
                $routeProvider.when('/scaffold/smartadmin/', {
                    templateUrl: '/scaffold/smartadmin/partials/Index',
                    controller: 'IndexCtrl'
                }).when('/scaffold/smartadmin/graph', {
                    templateUrl: '/scaffold/smartadmin/partials/Graph',
                    controller: 'GraphCtrl'
                }).otherwise({
                    templateUrl: '/scaffold/smartadmin/partials/NotFound',
                    controller: 'StaticCtrl'
                });
            }]).run(function () {
            console.log("run");
        });
    })(Smartadmin.Demo || (Smartadmin.Demo = {}));
    var Demo = Smartadmin.Demo;
})(Smartadmin || (Smartadmin = {}));
//# sourceMappingURL=App.js.map
