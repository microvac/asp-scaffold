/// <reference path="../../../typings/angularjs/angular.d.ts"/>
/// <reference path="App.ts"/>
var Smartadmin;
(function (Smartadmin) {
    /**
    * The main controller for the app. The controller:
    * - retrieves and persists the model via the todoStorage service
    * - exposes the model to the template and provides event handlers
    */
    (function (Demo) {
        var IndexCtrl = (function () {
            function IndexCtrl() {
            }
            return IndexCtrl;
        })();
        Demo.app.controller("IndexCtrl", IndexCtrl);

        var StaticCtrl = (function () {
            function StaticCtrl() {
            }
            return StaticCtrl;
        })();
        Demo.app.controller("StaticCtrl", StaticCtrl);

        var NavCtrl = (function () {
            function NavCtrl($scope, $location) {
                this.$scope = $scope;
                this.$location = $location;
                this.basePath = "/scaffold/smartadmin";
                $scope.navCtrl = this;
            }
            NavCtrl.prototype.getClass = function (path) {
                if (path == "" && this.$location.path() == this.basePath)
                    return "active";
                var fullPath = this.basePath + "/" + path;
                if (this.$location.path() == fullPath) {
                    return "active";
                }
                return "";
            };
            return NavCtrl;
        })();
        Demo.app.controller("NavCtrl", NavCtrl);

        /* chart colors default */
        var $chrt_border_color = "#efefef";
        var $chrt_grid_color = "#DDD";
        var $chrt_main = "#E24913";
        var $chrt_second = "#6595b4";
        var $chrt_third = "#FF9F01";
        var $chrt_fourth = "#7e9d3a";
        var $chrt_fifth = "#BD362F";
        var $chrt_mono = "#000";

        var flotOptions = {
            series: {
                lines: {
                    show: true
                }
            },
            grid: {
                hoverable: true,
                clickable: true,
                tickColor: $chrt_border_color,
                borderWidth: 0,
                borderColor: $chrt_border_color
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
            }
        };

        var RandomDataGenerator = (function () {
            function RandomDataGenerator(totalPoints) {
                this.totalPoints = totalPoints;
                this.data = [];
            }
            RandomDataGenerator.prototype.generate = function () {
                var data = this.data;

                if (data.length > 0)
                    this.data = data = data.slice(1);

                while (data.length < this.totalPoints) {
                    var prev = data.length > 0 ? data[data.length - 1] : 50, y = prev + Math.random() * 10 - 5;

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
            };
            return RandomDataGenerator;
        })();

        var GraphCtrl = (function () {
            function GraphCtrl($scope) {
                this.$scope = $scope;
                var updateInterval = 30;
                var generator1 = new RandomDataGenerator(100);
                var generator2 = new RandomDataGenerator(100);

                function update() {
                    $scope.$apply(function () {
                        $scope.flotData.data = [generator1.generate(), generator2.generate()];
                    });
                    setTimeout(update, updateInterval);
                }

                $scope.flotData = {
                    data: [generator1.generate(), generator2.generate()],
                    options: flotOptions
                };

                setTimeout(update, updateInterval);
            }
            return GraphCtrl;
        })();
        Demo.app.controller("GraphCtrl", GraphCtrl);
    })(Smartadmin.Demo || (Smartadmin.Demo = {}));
    var Demo = Smartadmin.Demo;
})(Smartadmin || (Smartadmin = {}));
//# sourceMappingURL=Controllers.js.map
