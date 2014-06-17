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
        var Anu = (function () {
            function Anu(data) {
                this.ID = data ? data.ID : null;
                this.Satu = data ? data.Satu : null;
                this.Dua = data ? data.Dua : null;
            }
            /* App.Controllers.AnuController */
            Anu.GetAll = function () {
                var res = $.ajax(Anu.ajaxSettings.build({
                    type: 'GET',
                    url: '/api/Anu/GetAll'
                })).then(function (models) {
                    return models.map(function (model) {
                        return new Anu(model);
                    });
                });
                return res;
            };

            Anu.Get = function (id) {
                var res = $.ajax(Anu.ajaxSettings.build({
                    type: 'GET',
                    url: '/api/Anu/Get/' + id
                })).then(function (model) {
                    return new Anu(model);
                });
                return res;
            };

            Anu.prototype.Save = function () {
                var _this = this;
                var isNew = this.ID == null;
                var model = this;
                var res = $.ajax(Anu.ajaxSettings.build({
                    type: isNew ? 'POST' : 'PUT',
                    url: '/api/Anu/' + (isNew ? 'Post' : 'Put'),
                    data: JSON.stringify(this)
                })).then(function (id) {
                    if (isNew) {
                        _this.ID = id;
                    }
                });
                return res;
            };

            Anu.prototype.Delete = function () {
                var res = $.ajax(Anu.ajaxSettings.build({
                    type: 'DELETE',
                    url: '/api/Anu/Delete/' + this.ID
                }));
                return res;
            };

            Anu.Delete = function (id) {
                var res = $.ajax(Anu.ajaxSettings.build({
                    type: 'GET',
                    url: '/api/Anu/Delete/' + id
                }));
                return res;
            };

            Anu.prototype.PostCount = function () {
                var res = $.ajax(Anu.ajaxSettings.build({
                    type: 'POST',
                    url: '/api/Anu/PostCount',
                    data: JSON.stringify(this)
                }));
                return res;
            };
            Anu.ajaxSettings = new Scaffold.AjaxSettings();
            return Anu;
        })();
        Models.Anu = Anu;

        var Model2 = (function () {
            function Model2(data) {
                this.ID = data ? data.ID : null;
                this.Description = data ? data.Description : null;
            }
            Model2.ajaxSettings = new Scaffold.AjaxSettings();
            return Model2;
        })();
        Models.Model2 = Model2;

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

            Todo.GetAduh = function () {
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: 'GET',
                    url: '/api/Todo/GetAduh'
                }));
                return res;
            };

            Todo.prototype.PostBody = function () {
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: 'POST',
                    url: '/api/Todo/PostBody',
                    data: JSON.stringify(this)
                }));
                return res;
            };

            Todo.PostBody2 = function (/** [FromBody] **/ todo) {
                var res = $.ajax(Todo.ajaxSettings.build({
                    type: 'POST',
                    url: '/api/Todo/PostBody2',
                    data: JSON.stringify(todo)
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
