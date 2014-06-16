// WARNING: T4 generated file  (it is related to CodeToServerProxy)
//
/// <reference path="../typings/jquery/jquery.d.ts"/>
var Scaffold;
(function (Scaffold) {
    var AjaxSettings = (function () {
        function AjaxSettings() {
            this.async = true;
            this.cache = false;
            this.timeout = 2000;
        }
        AjaxSettings.prototype.build = function (settings) {
            return {
                async: this.async,
                cache: this.cache,
                timeout: this.timeout,
                dataType: 'json',
                contentType: 'application/json',
                type: settings.type,
                url: settings.url,
                data: settings.data
            };
        };
        return AjaxSettings;
    })();
    Scaffold.AjaxSettings = AjaxSettings;
})(Scaffold || (Scaffold = {}));

var App;
(function (App) {
    (function (Models) {
        

        var Todo = (function () {
            function Todo(data) {
                this.ID = data ? data.ID : null;
                this.Name = data ? data.Name : null;
                this.Description = data ? data.Description : null;
            }
            /* App.Controllers.TodoController */
            Todo.GetAll = function () {
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: 'GET',
                    url: '/api/Todo/GetAll'
                })).then(function (models) {
                    return models.map(function (model) {
                        return new Todo(model);
                    });
                });
                return res;
            };

            Todo.Get = function (id) {
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: 'GET',
                    url: '/api/Todo/Get/' + id
                })).then(function (model) {
                    return new Todo(model);
                });
                return res;
            };

            Todo.prototype.Save = function () {
                var _this = this;
                var isNew = this.ID == null;
                var model = this;
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: isNew ? 'POST' : 'PUT',
                    url: '/api/Todo/' + (isNew ? 'Post' : 'Put'),
                    data: JSON.stringify(this)
                })).then(function (id) {
                    if (isNew) {
                        _this.ID = id;
                    }
                });
                return res;
            };

            Todo.prototype.Delete = function () {
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: 'DELETE',
                    url: '/api/Todo/Delete/' + this.ID
                }));
                return res;
            };

            Todo.Delete = function (id) {
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: 'GET',
                    url: '/api/Todo/Delete/' + id
                }));
                return res;
            };

            Todo.prototype.GetAduh = function () {
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: 'GET',
                    url: '/api/Todo/GetAduh'
                }));
                return res;
            };
            Todo.ajaxSettings = new Scaffold.AjaxSettings();
            return Todo;
        })();
        Models.Todo = Todo;
    })(App.Models || (App.Models = {}));
    var Models = App.Models;
})(App || (App = {}));
//# sourceMappingURL=Models.js.map
