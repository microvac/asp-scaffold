//https://gist.github.com/pjstarifa/6210002
// Requires jQuery from http://jquery.com/
// and jQuerySparklines from http://omnipotent.net/jquery.sparkline

// AngularJS directives for jquery sparkline
angular.module('angular-sparkline', []);
angular.module('angular-sparkline')
    .directive('sparkline', [function () {
        'use strict';
return {
            restrict: 'A',
            //require: 'ngModel',
            compile: function(tElement,tAttrs,transclude){
                var render= function(scope,element,attrs){
                    var elem = element;
                    var a, b, c, d, e, f;
                    var tb = $(elem),
                    ub = tb.data("sparkline-type") || "bar";
                    if ("bar" == ub && (a = tb.data("sparkline-bar-color") || tb.css("color") || "#0000f0", b = tb.data("sparkline-height") || "26px", c = tb.data("sparkline-barwidth") || 5, d = tb.data("sparkline-barspacing") || 2, e = tb.data("sparkline-negbar-color") || "#A90329", f = tb.data("sparkline-barstacked-color") || ["#A90329", "#0099c6", "#98AA56", "#da532c", "#4490B1", "#6E9461", "#990099", "#B4CAD3"],
                        tb.sparkline("html", {
                        barColor: a,
                        type: ub,
                        height: b,
                        barWidth: c,
                        barSpacing: d,
                        stackedBarColor: f,
                        negBarColor: e,
                        zeroAxis: "false"
                    })));
                };
                return render;
            },
                //render();
        }
}]); 