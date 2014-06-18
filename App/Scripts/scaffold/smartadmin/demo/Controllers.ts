/// <reference path="../../../typings/angularjs/angular.d.ts"/>
/// <reference path="App.ts"/>

/**
 * The main controller for the app. The controller:
 * - retrieves and persists the model via the todoStorage service
 * - exposes the model to the template and provides event handlers
 */


module Smartadmin.Demo {
    class IndexCtrl {
    }
    app.controller("IndexCtrl", IndexCtrl);

    class StaticCtrl {
    }
    app.controller("StaticCtrl", StaticCtrl);

    class NavCtrl {
        basePath = "/scaffold/smartadmin";
        constructor(public $scope, public $location) {
            $scope.navCtrl = this;
        }
        getClass(path: string):string {
            if (path == "" && this.$location.path() == this.basePath)
                return "active";
            var fullPath = this.basePath + "/" + path;
            if (this.$location.path() == fullPath) {
              return "active"
            } 
            return ""
        }
    }
    app.controller("NavCtrl", NavCtrl);

    /* chart colors default */
    var $chrt_border_color = "#efefef";
    var $chrt_grid_color = "#DDD"
    var $chrt_main = "#E24913";			/* red       */
    var $chrt_second = "#6595b4";		/* blue      */
    var $chrt_third = "#FF9F01";		/* orange    */
    var $chrt_fourth = "#7e9d3a";		/* green     */
    var $chrt_fifth = "#BD362F";		/* dark red  */
    var $chrt_mono = "#000";

    var flotOptions = {
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

    class RandomDataGenerator {
        data: Array<number> = []
        constructor(public totalPoints) {
        }

        generate(): Array<any> {

            var data = this.data;

            if (data.length > 0)
                this.data = data = data.slice(1);

            // Do a random walk

            while (data.length < this.totalPoints) {

                var prev = data.length > 0 ? data[data.length - 1] : 50,
                    y = prev + Math.random() * 10 - 5;

                if (y < 0) {
                    y = 0;
                } else if (y > 100) {
                    y = 100;
                }

                data.push(y);
            }

            // Zip the generated y values with the x values

            var res = [];
            for (var i = 0; i < data.length; ++i) {
                res.push([i, data[i]]);
			}

            return res;
        }

    }

    class GraphCtrl {
        constructor(public $scope) {
            var updateInterval = 30;
            var generator1 = new RandomDataGenerator(100);
            var generator2 = new RandomDataGenerator(100);

            function update() {
                $scope.$apply(() => {
                    $scope.flotData = [generator1.generate(), generator2.generate()];
                });
                setTimeout(update, updateInterval);
            }

            $scope.flotOptions= flotOptions;
            $scope.flotData = [generator1.generate(), generator2.generate()];

            setTimeout(update, updateInterval);
        }
    }
    app.controller("GraphCtrl", GraphCtrl);

}