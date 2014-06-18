/// <reference path="../../../typings/angularjs/angular.d.ts"/>
/// <reference path="App.ts"/>
var Scaffold;
(function (Scaffold) {
    (function (Demo) {
        /**
        * The main controller for the app. The controller:
        * - retrieves and persists the model via the todoStorage service
        * - exposes the model to the template and provides event handlers
        */
        (function (Smartadmin) {
            var IndexCtrl = (function () {
                function IndexCtrl() {
                }
                return IndexCtrl;
            })();
            Smartadmin.app.controller("IndexCtrl", IndexCtrl);

            var StaticCtrl = (function () {
                function StaticCtrl() {
                }
                return StaticCtrl;
            })();
            Smartadmin.app.controller("StaticCtrl", StaticCtrl);

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
            Smartadmin.app.controller("NavCtrl", NavCtrl);

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
                    $scope.sparkline1 = [1300, 1877, 2500, 2577, 2000, 2100, 3000, 2700, 3631, 2471, 2700, 3631, 2471];
                    $scope.sparkline2 = [110, 150, 300, 130, 400, 240, 220, 310, 220, 300, 270, 210];
                    $scope.sparkline3 = [110, 150, 300, 130, 400, 240, 220, 310, 220, 300, 270, 210];

                    var updateInterval = 30;
                    var generator1 = new RandomDataGenerator(100);
                    var generator2 = new RandomDataGenerator(100);

                    function update() {
                        $scope.$apply(function () {
                            $scope.flotData = [generator1.generate(), generator2.generate()];
                        });
                        setTimeout(update, updateInterval);
                    }

                    $scope.flotOptions = flotOptions;
                    $scope.flotData = [generator1.generate(), generator2.generate()];

                    setTimeout(update, updateInterval);
                }
                return GraphCtrl;
            })();
            Smartadmin.app.controller("GraphCtrl", GraphCtrl);
        })(Demo.Smartadmin || (Demo.Smartadmin = {}));
        var Smartadmin = Demo.Smartadmin;
    })(Scaffold.Demo || (Scaffold.Demo = {}));
    var Demo = Scaffold.Demo;
})(Scaffold || (Scaffold = {}));
//# sourceMappingURL=Controllers.js.map
