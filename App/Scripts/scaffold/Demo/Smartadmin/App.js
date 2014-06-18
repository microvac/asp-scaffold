var Scaffold;
(function (Scaffold) {
    (function (Demo) {
        /// <reference path="../../../../../Scaffold/Scripts/typings/angularjs/angular.d.ts"/>
        (function (Smartadmin) {
            Smartadmin.app = angular.module('app', [
                'ngRoute',
                'scaffold'
            ]);

            Smartadmin.app.config([
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
        })(Demo.Smartadmin || (Demo.Smartadmin = {}));
        var Smartadmin = Demo.Smartadmin;
    })(Scaffold.Demo || (Scaffold.Demo = {}));
    var Demo = Scaffold.Demo;
})(Scaffold || (Scaffold = {}));
//# sourceMappingURL=App.js.map
