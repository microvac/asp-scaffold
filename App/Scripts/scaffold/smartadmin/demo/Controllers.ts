/// <reference path="../../../typings/angularjs/angular.d.ts"/>
/// <reference path="App.ts"/>

/**
 * The main controller for the app. The controller:
 * - retrieves and persists the model via the todoStorage service
 * - exposes the model to the template and provides event handlers
 */


module Smartadmin.Demo {
    app.controller("IndexController", function ($scope) {
    });
    app.controller("StaticController", function ($scope) {
    });
    app.controller("NavController", function ($scope, $location) {
        var basePath = "/scaffold/smartadmin/";
        $scope.getClass = function (path: string):string {
            if (path == "" && $location.path() == "/scaffold/smartadmin")
                return "active";
            var fullPath = basePath + path;
            if ($location.path() == fullPath) {
              return "active"
            } 
            return ""
        }
    });

    app.controller("GraphController", function ($scope) {
        var data = [[],[]],
            totalPoints = 300;

        function getRandomData(i) {

            if (data[i].length > 0)
                data[i] = data[i].slice(1);

            // Do a random walk

            while (data[i].length < totalPoints) {

                var prev = data[i].length > 0 ? data[i][data[i].length - 1] : 50,
                    y = prev + Math.random() * 10 - 5;

                if (y < 0) {
                    y = 0;
                } else if (y > 100) {
                    y = 100;
                }

                data[i].push(y);
            }

            // Zip the generated y values with the x values

            var res = [];
            for (var j = 0; j < data[i].length; ++j) {
                res.push([j, data[i][j]])
			}

            return res;
        }

        var updateInterval = 30;

        $scope.flotData = {};

        function update() {

            $scope.$apply(() => {
                $scope.flotData.data = [getRandomData(0), getRandomData(1)];
            });

            setTimeout(update, updateInterval);
        }

        /* chart colors default */
        var $chrt_border_color = "#efefef";
        var $chrt_grid_color = "#DDD"
		var $chrt_main = "#E24913";			/* red       */
        var $chrt_second = "#6595b4";		/* blue      */
        var $chrt_third = "#FF9F01";		/* orange    */
        var $chrt_fourth = "#7e9d3a";		/* green     */
        var $chrt_fifth = "#BD362F";		/* dark red  */
        var $chrt_mono = "#000";

        $scope.flotData.data = [getRandomData(0), getRandomData(1)];
        $scope.flotData.options = {
            series: {
                lines: {
                    show: true
                },
            },
            grid: {
                hoverable: true,
                clickable: true,
                tickColor: $chrt_border_color,
                borderWidth: 0,
                borderColor: $chrt_border_color,
            },
            tooltip: true,
            tooltipOpts: {
                //content : "Value <b>$x</b> Value <span>$y</span>",
                defaultTheme: false
            },
            colors: [$chrt_second, $chrt_fourth],
            yaxis: {
                min: 0,
                max: 100
            },
        };
        
        setTimeout(update, updateInterval);
    });

}