angular.module('scaffold').directive('flot', function() {
  return {
    restrict: 'E',
    template: '<div></div>',
    scope: {
      dataset: '=',
      options: '='
    },
    link: function(scope, element, attributes) {
      var height, init, onDatasetChanged, onOptionsChanged, plot, plotArea, width;
      plot = null;
      width = attributes.width || '100%';
      height = attributes.height || '100%';
      if (!scope.dataset) {
        scope.dataset = [];
      }
      if (!scope.options) {
        scope.options = {
          legend: {
            show: false
          }
        };
      }
      plotArea = $(element.children()[0]);
      plotArea.css({
        width: width,
        height: height
      });
      init = function() {
        return $.plot(plotArea, scope.dataset, scope.options);
      };
      onDatasetChanged = function(dataset) {
        if (plot) {
          plot.setData(dataset);
          plot.setupGrid();
          return plot.draw();
        } else {
          return plot = init();
        }
      };
      scope.$watch('dataset', onDatasetChanged, true);
      onOptionsChanged = function() {
        return plot = init();
      };
      return scope.$watch('options', onOptionsChanged, true);
    }
  };
});
