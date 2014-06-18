//https://gist.github.com/pjstarifa/6210002
// Requires jQuery from http://jquery.com/
// and jQuerySparklines from http://omnipotent.net/jquery.sparkline

// AngularJS directives for jquery sparkline
angular.module('scaffold')
    .directive('sparkline', [function () {
        'use strict';
return {
            restrict: 'E',
            //require: 'ngModel',
            compile: function(tElement,tAttrs,transclude){
                var render= function(scope,element,attrs){
                    var elem = element;
                    var a, b, c, d, e, f;
                    var $el = $(elem);
                    var type = attrs.type || "bar";
                    if (type == "bar"){
                        a = attrs.barColor || $el.css("color") || "#0000f0"; 
                        b = attrs.height || "26px"; 
                        c = attrs.barWidth || 5; 
                        d = attrs.barSpacing || 2; 
                        e = attrs.negBarColor || "#A90329"; 
                        f = attrs.stackedBarColor || ["#A90329", "#0099c6", "#98AA56", "#da532c", "#4490B1", "#6E9461", "#990099", "#B4CAD3"];
                        var options = {
                            barColor: a,
                            type: type,
                            height: b,
                            barWidth: c,
                            barSpacing: d,
                            negBarColor: e,
                            stackedBarColor: f,
                            zeroAxis: "false"
                        }
                        $el.css("width", "100px");
                        $el.css("height", "100px");
                        $el.sparkline(scope[attrs.dataset],options);
                    }
                };
                return render;
            },
                //render();
        }
}]); 