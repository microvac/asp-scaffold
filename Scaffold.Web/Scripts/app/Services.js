var App;
(function (App) {
    // WARNING: T4 Template generated Javascript proxy client objects (it depends on CodeToJSonData)
    //
    /// <reference path="../typings/jquery/jquery.d.ts"/>
    /// <reference path="Models.ts"/>
    (function (Services) {
        var TodoService = (function () {
            function TodoService(baseurl) {
                this.baseurl = baseurl;
                this.cache = false;
                this.timeout = 2000;
                this.async = true;
            }
            TodoService.prototype.GetAll = function () {
                var res = $.ajax({
                    cache: this.cache,
                    async: this.async,
                    timeout: this.timeout,
                    dataType: 'json',
                    contentType: 'application/json',
                    type: 'GET',
                    url: this.baseurl + 'api/Todo/GetAll'
                });
                return res;
            };

            TodoService.prototype.Get = function (id) {
                var res = $.ajax({
                    cache: this.cache,
                    async: this.async,
                    timeout: this.timeout,
                    dataType: 'json',
                    contentType: 'application/json',
                    type: 'GET',
                    url: this.baseurl + 'api/Todo/Get?id=' + id + ''
                });
                return res;
            };
            return TodoService;
        })();
        Services.TodoService = TodoService;
    })(App.Services || (App.Services = {}));
    var Services = App.Services;
})(App || (App = {}));
//# sourceMappingURL=Services.js.map
